using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using Mimirorg.Common.Exceptions;
using Mimirorg.Common.Extensions;
using Mimirorg.Common.Models;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Services
{
    public class TransportService : ITransportService
    {
        private readonly IMapper _mapper;
        private readonly ITransportRepository _transportRepository;
        private readonly IVersionService _versionService;
        private readonly ApplicationSettings _applicationSettings;

        public TransportService(IMapper mapper, IOptions<ApplicationSettings> applicationSettings, IVersionService versionService, ITransportRepository transportRepository)
        {
            _mapper = mapper;
            _versionService = versionService;
            _transportRepository = transportRepository;
            _applicationSettings = applicationSettings?.Value;
        }

        public async Task<TransportLibCm> Get(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new MimirorgBadRequestException("Can't get transport. The id is missing value.");

            var transportDm = await _transportRepository.Get(id);

            if (transportDm == null)
                throw new MimirorgNotFoundException($"There is no transport with id: {id}");

            var latestVersion = await _versionService.GetLatestVersion(transportDm);

            if (latestVersion != null && transportDm.Id != latestVersion.Id)
                throw new MimirorgBadRequestException($"The transport with id {id} and version {transportDm.Version} is older than latest version {latestVersion.Version}.");

            var transportLibCm = _mapper.Map<TransportLibCm>(transportDm);

            if (transportLibCm == null)
                throw new MimirorgMappingException("TransportLibDm", "TransportLibCm");

            return transportLibCm;
        }

        public async Task<IEnumerable<TransportLibCm>> GetLatestVersions()
        {
            var distinctFirstVersionIdDm = _transportRepository.Get()?.ToList().DistinctBy(x => x.FirstVersionId).ToList();

            if (distinctFirstVersionIdDm == null || !distinctFirstVersionIdDm.Any())
                return await Task.FromResult(new List<TransportLibCm>());

            var transports = new List<TransportLibDm>();

            foreach (var dm in distinctFirstVersionIdDm)
                transports.Add(await _versionService.GetLatestVersion(dm));

            transports = transports.OrderBy(x => x.Aspect).ThenBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList();

            var transportLibCms = _mapper.Map<List<TransportLibCm>>(transports);

            if (transports.Any() && (transportLibCms == null || !transportLibCms.Any()))
                throw new MimirorgMappingException("List<TransportLibDm>", "ICollection<TransportLibAm>");

            return await Task.FromResult(transportLibCms ?? new List<TransportLibCm>());
        }

        public async Task<TransportLibCm> Create(TransportLibAm dataAm)
        {
            if (dataAm == null)
                throw new MimirorgBadRequestException("Data object can not be null.");

            var existing = await _transportRepository.Get(dataAm.Id);

            if (existing != null)
                throw new MimirorgBadRequestException($"Transport '{existing.Name}' with RdsCode '{existing.RdsCode}', Aspect '{existing.Aspect}' and version '{existing.Version}' already exist in db.");

            var transportLibDm = _mapper.Map<TransportLibDm>(dataAm);

            if (!double.TryParse(transportLibDm.Version, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out _))
                throw new MimirorgBadRequestException($"Error when parsing version value '{transportLibDm.Version}' to double.");

            if (transportLibDm == null)
                throw new MimirorgMappingException("TransportLibAm", "TransportLibDm");

            await _transportRepository.Create(transportLibDm);
            _transportRepository.ClearAllChangeTrackers();

            return await Get(transportLibDm.Id);
        }

        public async Task<IEnumerable<TransportLibCm>> Create(IEnumerable<TransportLibAm> transports, bool createdBySystem = false)
        {
            var validation = transports.ValidateObject();

            if (!validation.IsValid)
                throw new MimirorgBadRequestException("Couldn't create transports", validation);

            var existingTransportTypes = await GetLatestVersions();
            var transportsToCreate = transports.Where(x => existingTransportTypes.All(y => y.Id != x.Id)).ToList();
            var transportDmList = _mapper.Map<IEnumerable<TransportLibDm>>(transportsToCreate).ToList();

            if (transportDmList == null || (!transportDmList.Any() && transportsToCreate.Any()))
                throw new MimirorgMappingException("ICollection<TransportLibDm>", "ICollection<TransportLibAm>");

            foreach (var transportLibDm in transportDmList)
            {
                if (!double.TryParse(transportLibDm.Version, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out _))
                    throw new MimirorgBadRequestException($"Error when parsing version value '{transportLibDm.Version}' to double.");

                transportLibDm.CreatedBy = createdBySystem ? _applicationSettings.System : transportLibDm.CreatedBy;
                await _transportRepository.Create(transportLibDm);
            }

            _transportRepository.ClearAllChangeTrackers();

            return _mapper.Map<ICollection<TransportLibCm>>(transportDmList);
        }

        public async Task<TransportLibCm> Update(TransportLibAm dataAm, string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new MimirorgBadRequestException("Can't update a transport without an id.");

            if (dataAm == null)
                throw new MimirorgBadRequestException("Can't update a transport when dataAm is null.");

            var transportToUpdate = await _transportRepository.Get(id);

            if (transportToUpdate?.Id == null)
                throw new MimirorgNotFoundException($"Transport with id {id} does not exist, update is not possible.");

            if (transportToUpdate.CreatedBy == _applicationSettings.System)
                throw new MimirorgBadRequestException($"The transport with id {id} is created by the system and can not be updated.");

            if (transportToUpdate.Deleted)
                throw new MimirorgBadRequestException($"The transport with id {id} is deleted and can not be updated.");

            var latestTransportDm = await _versionService.GetLatestVersion(transportToUpdate);

            if (latestTransportDm == null)
                throw new MimirorgBadRequestException($"Latest node version for node with id {id} not found (null).");

            if (string.IsNullOrWhiteSpace(latestTransportDm.Version))
                throw new MimirorgBadRequestException($"Latest version for node with id {id} has null or empty as version number.");

            var latestTransportVersion = double.Parse(latestTransportDm.Version, CultureInfo.InvariantCulture);
            var transportToUpdateVersion = double.Parse(transportToUpdate.Version, CultureInfo.InvariantCulture);

            if (latestTransportVersion > transportToUpdateVersion)
                throw new MimirorgBadRequestException($"Not allowed to update transport with id {transportToUpdate.Id} and version {transportToUpdateVersion}. Latest version is transport with id {latestTransportDm.Id} and version {latestTransportVersion}");

            dataAm.Version = await _versionService.CalculateNewVersion(latestTransportDm, dataAm);
            dataAm.FirstVersionId = latestTransportDm.FirstVersionId;

            return await Create(dataAm);
        }

        public async Task<bool> Delete(string id)
        {
            return await _transportRepository.Delete(id);
        }

        public async Task<bool> CompanyIsChanged(string transportId, int companyId)
        {
            var transport = await Get(transportId);

            if (transport == null)
                throw new MimirorgNotFoundException($"Couldn't find transport with id: {transportId}");

            return transport.CompanyId != companyId;
        }
    }
}