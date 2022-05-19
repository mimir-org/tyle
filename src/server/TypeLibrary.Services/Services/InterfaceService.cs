using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Mimirorg.Common.Exceptions;
using Mimirorg.Common.Extensions;
using Mimirorg.Common.Models;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Services
{
    public class InterfaceService : IInterfaceService
    {
        private readonly IMapper _mapper;
        private readonly IEfInterfaceRepository _interfaceRepository;
        private readonly IEfRdsRepository _rdsRepository;
        private readonly IEfAttributeRepository _attributeRepository;
        private readonly IEfPurposeRepository _purposeRepository;
        private readonly ApplicationSettings _applicationSettings;

        public InterfaceService(IEfPurposeRepository purposeRepository, IEfAttributeRepository attributeRepository, IEfRdsRepository rdsRepository, IMapper mapper, IEfInterfaceRepository interfaceRepository, IOptions<ApplicationSettings> applicationSettings)
        {
            _purposeRepository = purposeRepository;
            _attributeRepository = attributeRepository;
            _rdsRepository = rdsRepository;
            _mapper = mapper;
            _interfaceRepository = interfaceRepository;
            _applicationSettings = applicationSettings?.Value;
        }

        public async Task<InterfaceLibCm> GetInterface(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new MimirorgBadRequestException("Can't get interface. The id is missing value.");

            var data = await _interfaceRepository.FindInterface(id).FirstOrDefaultAsync();

            if (data == null)
                throw new MimirorgNotFoundException($"There is no interface with id: {id}");

            if (data.Deleted)
                throw new MimirorgBadRequestException($"The item with id {id} is marked as deleted in the database.");

            var interfaceLibCm = _mapper.Map<InterfaceLibCm>(data);

            if (interfaceLibCm == null)
                throw new MimirorgMappingException("InterfaceLibDm", "InterfaceLibCm");

            return interfaceLibCm;
        }

        public Task<IEnumerable<InterfaceLibCm>> GetInterfaces()
        {
            var interfaces = _interfaceRepository.GetAllInterfaces().Where(x => !x.Deleted).ToList()
                .OrderBy(x => x.Aspect)
                .ThenBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList();

            var interfaceLibCms = _mapper.Map<IEnumerable<InterfaceLibCm>>(interfaces);

            if (interfaces.Any() && (interfaceLibCms == null || !interfaceLibCms.Any()))
                throw new MimirorgMappingException("List<InterfaceLibDm>", "ICollection<InterfaceLibAm>");

            return Task.FromResult(interfaceLibCms ?? new List<InterfaceLibCm>());
        }

        public async Task<InterfaceLibCm> CreateInterface(InterfaceLibAm dataAm)
        {
            if (dataAm == null)
                throw new MimirorgBadRequestException("Data object can not be null.");

            var existing = await _interfaceRepository.GetAsync(dataAm.Id);

            if (existing != null)
            {
                var errorText = $"Node '{existing.Name}', with RdsCode '{existing.RdsCode}', Aspect '{existing.Aspect}' and version '{existing.Version}' already exist in db";

                throw existing.Deleted switch
                {
                    false => new MimirorgBadRequestException(errorText),
                    true => new MimirorgBadRequestException(errorText + " as deleted")
                };
            }

            var interfaceLibDm = _mapper.Map<InterfaceLibDm>(dataAm);

            if (!double.TryParse(interfaceLibDm.Version, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out _))
                throw new MimirorgBadRequestException($"Error when parsing version value '{interfaceLibDm.Version}' to double.");

            if (interfaceLibDm == null)
                throw new MimirorgMappingException("InterfaceLibAm", "InterfaceLibDm");

            if (interfaceLibDm.Attributes != null && interfaceLibDm.Attributes.Any())
                _attributeRepository.Attach(interfaceLibDm.Attributes, EntityState.Unchanged);

            await _interfaceRepository.CreateAsync(interfaceLibDm);
            await _interfaceRepository.SaveAsync();

            if (interfaceLibDm.Attributes != null && interfaceLibDm.Attributes.Any())
                _attributeRepository.Detach(interfaceLibDm.Attributes);

            _interfaceRepository.Detach(interfaceLibDm);

            return await GetInterface(interfaceLibDm.Id);
        }

        public async Task<InterfaceLibCm> UpdateInterface(InterfaceLibAm dataAm, string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new MimirorgBadRequestException("Can't update an interface without an id.");

            if (dataAm == null)
                throw new MimirorgBadRequestException("Can't update an interface when dataAm is null.");

            if (id == dataAm.Id)
                throw new MimirorgBadRequestException("Not allowed to update: Name, RdsCode or Aspect.");

            var interfaceToUpdate = await _interfaceRepository.FindInterface(id).FirstOrDefaultAsync();

            if (interfaceToUpdate?.Id == null)
                throw new MimirorgNotFoundException($"Interface with id {id} does not exist.");

            if (interfaceToUpdate.CreatedBy == _applicationSettings.System)
                throw new MimirorgBadRequestException($"The interface with id {id} is created by the system and can not be updated.");

            if (interfaceToUpdate.Deleted)
                throw new MimirorgBadRequestException($"The transport with id {id} is deleted and can not be updated.");

            var latestInterfaceDm = GetLatestInterfaceVersion(interfaceToUpdate.FirstVersionId);

            var latestInterfaceVersion = double.Parse(latestInterfaceDm.Version, CultureInfo.InvariantCulture);
            var interfaceToUpdateVersion = double.Parse(interfaceToUpdate.Version, CultureInfo.InvariantCulture);

            if (latestInterfaceVersion > interfaceToUpdateVersion)
                throw new MimirorgBadRequestException($"Not allowed to update interface with id {interfaceToUpdate.Id} and version {interfaceToUpdateVersion}. Latest version is interface with id {latestInterfaceDm.Id} and version {latestInterfaceVersion}");

            dataAm.Version = IncrementInterfaceVersion(latestInterfaceDm, dataAm);
            dataAm.FirstVersionId = latestInterfaceDm.FirstVersionId;

            return await CreateInterface(dataAm);
        }

        public async Task<bool> DeleteInterface(string id)
        {
            var dm = await _interfaceRepository.GetAsync(id);

            if (dm.Deleted)
                throw new MimirorgBadRequestException($"The interface with id {id} is already marked as deleted in the database.");

            if (dm.CreatedBy == _applicationSettings.System)
                throw new MimirorgBadRequestException($"The interface with id {id} is created by the system and can not be deleted.");

            dm.Deleted = true;

            var status = await _interfaceRepository.Context.SaveChangesAsync();
            return status == 1;
        }

        public void ClearAllChangeTrackers()
        {
            _interfaceRepository?.Context?.ChangeTracker.Clear();
            _rdsRepository?.Context?.ChangeTracker.Clear();
            _attributeRepository?.Context?.ChangeTracker.Clear();
            _purposeRepository?.Context?.ChangeTracker.Clear();
        }

        #region Private

        private InterfaceLibDm GetLatestInterfaceVersion(string firstVersionId)
        {
            var existingDmVersions = _interfaceRepository.GetAllInterfaces()
                .Where(x => x.FirstVersionId == firstVersionId && !x.Deleted).ToList()
                .OrderBy(x => double.Parse(x.Version, CultureInfo.InvariantCulture)).ToList();

            if (!existingDmVersions.Any())
                throw new MimirorgBadRequestException($"No interfaces with 'FirstVersionId' {firstVersionId} found.");

            return existingDmVersions[^1];
        }

        // ReSharper disable once ReplaceWithSingleAssignment.False
        // ReSharper disable once ConvertIfToOrExpression
        private static string IncrementInterfaceVersion(InterfaceLibDm existing, InterfaceLibAm updated)
        {
            var major = false;
            var minor = false;

            if (existing.Name != updated.Name)
                throw new MimirorgBadRequestException("You cannot change existing name when updating.");

            if (existing.RdsCode != updated.RdsCode)
                throw new MimirorgBadRequestException("You cannot change existing RDS code when updating.");

            if (existing.RdsName != updated.RdsName)
                throw new MimirorgBadRequestException("You cannot change existing RDS code when updating.");

            if (existing.Aspect != updated.Aspect)
                throw new MimirorgBadRequestException("You cannot change existing Aspect when updating.");

            //PurposeName
            if (existing.PurposeName != updated.PurposeName)
                minor = true;

            //CompanyId
            if (existing.CompanyId != updated.CompanyId)
                minor = true;

            //TerminalId
            if (existing.TerminalId != updated.TerminalId)
                throw new MimirorgBadRequestException("You cannot change existing terminal id when updating.");

            //Attribute
            var attributeIdDmList = existing.Attributes?.Select(x => x.Id).ToList().OrderBy(x => x, StringComparer.InvariantCulture).ToList();
            var attributeIdAmList = updated.AttributeIdList?.ToList().OrderBy(x => x, StringComparer.InvariantCulture).ToList();

            if (attributeIdAmList?.Count < attributeIdDmList?.Count)
                throw new MimirorgBadRequestException("You cannot remove existing attributes when updating, only add.");

            if (attributeIdAmList?.Count >= attributeIdDmList?.Count)
            {
                if (attributeIdDmList.Where(x => attributeIdAmList.Any(y => y == x)).ToList().Count != attributeIdDmList.Count)
                    throw new MimirorgBadRequestException("You cannot change existing attributes when updating, only add.");
            }

            if (attributeIdAmList?.Count > attributeIdDmList?.Count)
                major = true;

            //Description
            if (existing.Description != updated.Description)
                minor = true;

            //ContentReferences
            var contentRefsAm = updated.ContentReferences?.ToList().OrderBy(x => x, StringComparer.InvariantCulture).ToList();
            var contentRefsDm = existing.ContentReferences?.ConvertToArray().ToList().OrderBy(x => x, StringComparer.InvariantCulture).ToList();

            if (contentRefsAm?.Count != contentRefsDm?.Count)
                minor = true;

            if (contentRefsAm != null && contentRefsDm != null && contentRefsAm.Count == contentRefsDm.Count)
            {
                if (contentRefsDm.Where((t, i) => t != contentRefsAm[i]).Any())
                    minor = true;
            }

            //ParentId
            if (existing.ParentId != updated.ParentId)
                throw new MimirorgBadRequestException("You cannot change existing parent id when updating.");

            if (!major && !minor)
                throw new MimirorgBadRequestException("Existing interface and updated interface is identical");

            return major ? existing.Version.IncrementMajorVersion() : existing.Version.IncrementMinorVersion();
        }

        #endregion Private
    }
}