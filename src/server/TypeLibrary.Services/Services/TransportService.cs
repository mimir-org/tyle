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
    public class TransportService : ITransportService
    {
        private readonly IMapper _mapper;
        private readonly IEfTransportRepository _transportRepository;
        private readonly IEfRdsRepository _rdsRepository;
        private readonly IEfAttributeRepository _attributeRepository;
        private readonly IEfPurposeRepository _purposeRepository;
        private readonly ApplicationSettings _applicationSettings;

        public TransportService(IEfPurposeRepository purposeRepository, IEfAttributeRepository attributeRepository, IEfRdsRepository rdsRepository, IEfTransportRepository transportRepository, IMapper mapper, IOptions<ApplicationSettings> applicationSettings)
        {
            _purposeRepository = purposeRepository;
            _attributeRepository = attributeRepository;
            _rdsRepository = rdsRepository;
            _transportRepository = transportRepository;
            _mapper = mapper;
            _applicationSettings = applicationSettings?.Value;
        }

        public async Task<TransportLibCm> GetTransport(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new MimirorgBadRequestException("Can't get transport. The id is missing value.");

            var transport = await _transportRepository.FindTransport(id).FirstOrDefaultAsync();

            if (transport == null)
                throw new MimirorgNotFoundException($"There is no transport with id: {id}");

            if (transport.Deleted)
                throw new MimirorgBadRequestException($"The item with id {id} is marked as deleted in the database.");

            var transportLibCm = _mapper.Map<TransportLibCm>(transport);

            if (transportLibCm == null)
                throw new MimirorgMappingException("TransportLibDm", "TransportLibCm");

            return transportLibCm;
        }

        public Task<IEnumerable<TransportLibCm>> GetTransports()
        {
            var transports = _transportRepository.GetAllTransports().Where(x => !x.Deleted).ToList()
                .OrderBy(x => x.Aspect)
                .ThenBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList();

            var transportLibCms = _mapper.Map<IEnumerable<TransportLibCm>>(transports);

            if (transports.Any() && (transportLibCms == null || !transportLibCms.Any()))
                throw new MimirorgMappingException("List<TransportLibDm>", "ICollection<TransportLibAm>");

            return Task.FromResult(transportLibCms ?? new List<TransportLibCm>());
        }

        public async Task<TransportLibCm> CreateTransport(TransportLibAm dataAm)
        {
            if (dataAm == null)
                throw new MimirorgBadRequestException("Data object can not be null.");

            var existing = await _transportRepository.GetAsync(dataAm.Id);

            if (existing != null)
            {
                var errorText = $"Transport '{existing.Name}' with RdsCode '{existing.RdsCode}', Aspect '{existing.Aspect}' and version '{existing.Version}' already exist in db";

                throw existing.Deleted switch
                {
                    false => new MimirorgBadRequestException(errorText),
                    true => new MimirorgBadRequestException(errorText + " as deleted")
                };
            }

            var transportLibDm = _mapper.Map<TransportLibDm>(dataAm);

            if (!double.TryParse(transportLibDm.Version, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out _))
                throw new MimirorgBadRequestException($"Error when parsing version value '{transportLibDm.Version}' to double.");

            if (transportLibDm == null)
                throw new MimirorgMappingException("TransportLibAm", "TransportLibDm");

            if (transportLibDm.Attributes != null && transportLibDm.Attributes.Any())
                _attributeRepository.Attach(transportLibDm.Attributes, EntityState.Unchanged);

            await _transportRepository.CreateAsync(transportLibDm);
            await _transportRepository.SaveAsync();

            if (transportLibDm.Attributes != null && transportLibDm.Attributes.Any())
                _attributeRepository.Detach(transportLibDm.Attributes);

            _transportRepository.Detach(transportLibDm);

            return await GetTransport(transportLibDm.Id);
        }

        public async Task<IEnumerable<TransportLibCm>> CreateTransports(IEnumerable<TransportLibAm> transports, bool createdBySystem = false)
        {
            var validation = transports.ValidateObject();

            if (!validation.IsValid)
                throw new MimirorgBadRequestException("Couldn't create transports", validation);

            var existingTransportTypes = await GetTransports();
            var transportsToCreate = transports.Where(x => existingTransportTypes.All(y => y.Id != x.Id)).ToList();
            var transportDmList = _mapper.Map<IEnumerable<TransportLibDm>>(transportsToCreate).ToList();

            if (transportDmList == null || (!transportDmList.Any() && transportsToCreate.Any()))
                throw new MimirorgMappingException("ICollection<TransportLibDm>", "ICollection<TransportLibAm>");

            var allRds = _rdsRepository.GetAll();
            var allPurposes = _purposeRepository.GetAll();

            foreach (var transportLibDm in transportDmList)
            {
                if (!double.TryParse(transportLibDm.Version, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out _))
                    throw new MimirorgBadRequestException($"Error when parsing version value '{transportLibDm.Version}' to double.");

                transportLibDm.RdsName = allRds.FirstOrDefault(x => x.Id == transportLibDm.RdsCode)?.Name;
                transportLibDm.PurposeName = allPurposes.FirstOrDefault(x => x.Id == transportLibDm.PurposeName)?.Name;

                _attributeRepository.Attach(transportLibDm.Attributes, EntityState.Unchanged);

                transportLibDm.CreatedBy = createdBySystem ? _applicationSettings.System : transportLibDm.CreatedBy;

                await _transportRepository.CreateAsync(transportLibDm);
                await _transportRepository.SaveAsync();

                _attributeRepository.Detach(transportLibDm.Attributes);
                _transportRepository.Detach(transportLibDm);
            }

            return _mapper.Map<ICollection<TransportLibCm>>(transportDmList);
        }

        public async Task<TransportLibCm> UpdateTransport(TransportLibAm dataAm, string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new MimirorgBadRequestException("Can't update a transport without an id.");

            if (dataAm == null)
                throw new MimirorgBadRequestException("Can't update a transport when dataAm is null.");

            var transportToUpdate = await _transportRepository.FindTransport(id).FirstOrDefaultAsync();

            if (transportToUpdate?.Id == null)
                throw new MimirorgNotFoundException($"Transport with id {id} does not exist, update is not possible.");

            if (transportToUpdate.CreatedBy == _applicationSettings.System)
                throw new MimirorgBadRequestException($"The transport with id {id} is created by the system and can not be updated.");

            if (transportToUpdate.Deleted)
                throw new MimirorgBadRequestException($"The transport with id {id} is deleted and can not be updated.");

            var latestTransportDm = GetLatestTransportVersion(transportToUpdate.FirstVersionId);

            var latestTransportVersion = double.Parse(latestTransportDm.Version, CultureInfo.InvariantCulture);
            var transportToUpdateVersion = double.Parse(transportToUpdate.Version, CultureInfo.InvariantCulture);

            if (latestTransportVersion > transportToUpdateVersion)
                throw new MimirorgBadRequestException($"Not allowed to update transport with id {transportToUpdate.Id} and version {transportToUpdateVersion}. Latest version is transport with id {latestTransportDm.Id} and version {latestTransportVersion}");

            dataAm.Version = IncrementTransportVersion(latestTransportDm, dataAm);
            dataAm.FirstVersionId = latestTransportDm.FirstVersionId;

            return await CreateTransport(dataAm);
        }

        public async Task<bool> DeleteTransport(string id)
        {
            var dm = await _transportRepository.GetAsync(id);

            if (dm.Deleted)
                throw new MimirorgBadRequestException($"The transport with id {id} is already marked as deleted in the database.");

            if (dm.CreatedBy == _applicationSettings.System)
                throw new MimirorgBadRequestException($"The transport with id {id} is created by the system and can not be deleted.");

            dm.Deleted = true;

            var status = await _transportRepository.Context.SaveChangesAsync();
            return status == 1;
        }

        public void ClearAllChangeTrackers()
        {
            _transportRepository?.Context?.ChangeTracker.Clear();
            _rdsRepository?.Context?.ChangeTracker.Clear();
            _attributeRepository?.Context?.ChangeTracker.Clear();
            _purposeRepository?.Context?.ChangeTracker.Clear();
        }

        #region Private

        private TransportLibDm GetLatestTransportVersion(string firstVersionId)
        {
            var existingDmVersions = _transportRepository.GetAllTransports()
                .Where(x => x.FirstVersionId == firstVersionId && !x.Deleted).ToList()
                .OrderBy(x => double.Parse(x.Version, CultureInfo.InvariantCulture)).ToList();

            if (!existingDmVersions.Any())
                throw new MimirorgBadRequestException($"No nodes with 'FirstVersionId' {firstVersionId} found.");

            return existingDmVersions[^1];
        }

        // ReSharper disable once ReplaceWithSingleAssignment.False
        // ReSharper disable once ConvertIfToOrExpression
        private static string IncrementTransportVersion(TransportLibDm existing, TransportLibAm updated)
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
                if (attributeIdDmList.Where(x => attributeIdAmList.Any(y => y == x)).ToList().Count() != attributeIdDmList.Count)
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
                throw new MimirorgBadRequestException("Existing transport and updated transport is identical");

            return major ? existing.Version.IncrementMajorVersion() : existing.Version.IncrementMinorVersion();
        }

        #endregion Private
    }
}