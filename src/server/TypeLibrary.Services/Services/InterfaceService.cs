using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Common.Exceptions;
using Mimirorg.Common.Models;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Services
{
    public class InterfaceService : IInterfaceService
    {
        private readonly IMapper _mapper;
        private readonly IInterfaceRepository _interfaceRepository;
        private readonly IVersionService _versionService;
        private readonly ITimedHookService _hookService;
        private readonly ApplicationSettings _applicationSettings;

        public InterfaceService(IMapper mapper, IOptions<ApplicationSettings> applicationSettings, IVersionService versionService, IInterfaceRepository interfaceRepository, ITimedHookService hookService)
        {
            _mapper = mapper;
            _versionService = versionService;
            _interfaceRepository = interfaceRepository;
            _hookService = hookService;
            _applicationSettings = applicationSettings?.Value;
        }

        public async Task<InterfaceLibCm> Get(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new MimirorgBadRequestException("Can't get interface. The id is missing value.");

            var interfaceDm = await _interfaceRepository.Get(id);

            if (interfaceDm == null)
                throw new MimirorgNotFoundException($"There is no interface with id: {id}");

            var latestVersion = await _versionService.GetLatestVersion(interfaceDm);

            if (latestVersion != null && interfaceDm.Id != latestVersion.Id)
                throw new MimirorgBadRequestException($"The interface with id {id} and version {interfaceDm.Version} is older than latest version {latestVersion.Version}.");

            var interfaceLibCm = _mapper.Map<InterfaceLibCm>(interfaceDm);

            if (interfaceLibCm == null)
                throw new MimirorgMappingException("InterfaceLibDm", "InterfaceLibCm");

            return interfaceLibCm;
        }

        public async Task<IEnumerable<InterfaceLibCm>> GetLatestVersions()
        {
            var distinctFirstVersionIdDm = _interfaceRepository.Get()?.ToList().DistinctBy(x => x.FirstVersionId).ToList();

            if (distinctFirstVersionIdDm == null || !distinctFirstVersionIdDm.Any())
                return await Task.FromResult(new List<InterfaceLibCm>());

            var interfaces = new List<InterfaceLibDm>();

            foreach (var dm in distinctFirstVersionIdDm)
                interfaces.Add(await _versionService.GetLatestVersion(dm));

            interfaces = interfaces.OrderBy(x => x.Aspect).ThenBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList();

            var interfaceLibCms = _mapper.Map<List<InterfaceLibCm>>(interfaces);

            if (interfaces.Any() && (interfaceLibCms == null || !interfaceLibCms.Any()))
                throw new MimirorgMappingException("List<InterfaceLibDm>", "ICollection<InterfaceLibAm>");

            return await Task.FromResult(interfaceLibCms ?? new List<InterfaceLibCm>());
        }

        public async Task<InterfaceLibCm> Create(InterfaceLibAm dataAm)
        {
            if (dataAm == null)
                throw new MimirorgBadRequestException("Data object can not be null.");

            var existing = await _interfaceRepository.Get(dataAm.Id);

            if (existing != null)
                throw new MimirorgBadRequestException($"Node '{existing.Name}', with RdsCode '{existing.RdsCode}', Aspect '{existing.Aspect}' and version '{existing.Version}' already exist in db.");

            var interfaceLibDm = _mapper.Map<InterfaceLibDm>(dataAm);

            if (!double.TryParse(interfaceLibDm.Version, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out _))
                throw new MimirorgBadRequestException($"Error when parsing version value '{interfaceLibDm.Version}' to double.");

            if (interfaceLibDm == null)
                throw new MimirorgMappingException("InterfaceLibAm", "InterfaceLibDm");

            await _interfaceRepository.Create(interfaceLibDm);
            _interfaceRepository.ClearAllChangeTrackers();

            var dm = await Get(interfaceLibDm.Id);

            if (dm != null)
                _hookService.HookQueue.Enqueue(CacheKey.Interface);

            return dm;
        }

        public async Task<InterfaceLibCm> Update(InterfaceLibAm dataAm, string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new MimirorgBadRequestException("Can't update an interface without an id.");

            if (dataAm == null)
                throw new MimirorgBadRequestException("Can't update an interface when dataAm is null.");

            if (id == dataAm.Id)
                throw new MimirorgBadRequestException("Not allowed to update: Name, RdsCode or Aspect.");

            var interfaceToUpdate = await _interfaceRepository.Get(id);

            if (interfaceToUpdate?.Id == null)
                throw new MimirorgNotFoundException($"Interface with id {id} does not exist.");

            if (interfaceToUpdate.CreatedBy == _applicationSettings.System)
                throw new MimirorgBadRequestException($"The interface with id {id} is created by the system and can not be updated.");

            if (interfaceToUpdate.Deleted)
                throw new MimirorgBadRequestException($"The transport with id {id} is deleted and can not be updated.");

            var latestInterfaceDm = await _versionService.GetLatestVersion(interfaceToUpdate);

            if (latestInterfaceDm == null)
                throw new MimirorgBadRequestException($"Latest node version for node with id {id} not found (null).");

            if (string.IsNullOrWhiteSpace(latestInterfaceDm.Version))
                throw new MimirorgBadRequestException($"Latest version for node with id {id} has null or empty as version number.");

            var latestInterfaceVersion = double.Parse(latestInterfaceDm.Version, CultureInfo.InvariantCulture);
            var interfaceToUpdateVersion = double.Parse(interfaceToUpdate.Version, CultureInfo.InvariantCulture);

            if (latestInterfaceVersion > interfaceToUpdateVersion)
                throw new MimirorgBadRequestException($"Not allowed to update interface with id {interfaceToUpdate.Id} and version {interfaceToUpdateVersion}. Latest version is interface with id {latestInterfaceDm.Id} and version {latestInterfaceVersion}");

            var newVersion = await _versionService.CalculateNewVersion(latestInterfaceDm, dataAm);

            if (string.IsNullOrWhiteSpace(newVersion))
                return await Get(id);

            dataAm.Version = newVersion;
            dataAm.FirstVersionId = latestInterfaceDm.FirstVersionId;

            return await Create(dataAm);
        }

        public async Task<bool> Delete(string id)
        {
            var deleted = await _interfaceRepository.Delete(id);

            if (deleted)
                _hookService.HookQueue.Enqueue(CacheKey.Interface);

            return deleted;
        }

        public async Task<bool> CompanyIsChanged(string interfaceId, int companyId)
        {
            var node = await Get(interfaceId);

            if (node == null)
                throw new MimirorgNotFoundException($"Couldn't find interface with id: {interfaceId}");

            return node.CompanyId != companyId;
        }
    }
}