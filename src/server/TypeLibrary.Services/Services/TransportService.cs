using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Common.Enums;
using Mimirorg.Common.Exceptions;
using Mimirorg.Common.Extensions;
using Mimirorg.Common.Models;
using Mimirorg.TypeLibrary.Enums;
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
        private readonly ITimedHookService _hookService;

        public TransportService(IMapper mapper, ITransportRepository transportRepository, ITimedHookService hookService)
        {
            _mapper = mapper;
            _transportRepository = transportRepository;
            _hookService = hookService;
        }

        /// <summary>
        /// Get the latest version of a transport based on given id
        /// </summary>
        /// <param name="id">The id of the transport</param>
        /// <returns>The latest version of the transport of given id</returns>
        /// <exception cref="MimirorgNotFoundException">Throws if there is no transport with the given id, and that transport is at the latest version.</exception>
        public TransportLibCm GetLatestVersion(string id)
        {
            var transportCm = GetLatestVersions().FirstOrDefault(x => x.Id == id);

            if (transportCm == null)
                throw new MimirorgNotFoundException($"There is no transport with id {id}");

            return transportCm;
        }

        /// <summary>
        /// Get the latest transport versions
        /// </summary>
        /// <returns>A collection of transport</returns>
        public IEnumerable<TransportLibCm> GetLatestVersions()
        {
            var transportDmList = _transportRepository.Get().LatestVersion().ToList();
            transportDmList = transportDmList.OrderBy(x => x.Aspect).ThenBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList();

            foreach (var transportLibDm in transportDmList)
                transportLibDm.Children = transportDmList.Where(x => x.ParentId == transportLibDm.Id).ToList();

            return !transportDmList.Any() ? new List<TransportLibCm>() : _mapper.Map<List<TransportLibCm>>(transportDmList);
        }

        /// <summary>
        /// Create a new transport
        /// </summary>
        /// <param name="transportAm">The transport that should be created</param>
        /// <returns>The created transport</returns>
        /// <exception cref="MimirorgBadRequestException">Throws if transport is not valid</exception>
        /// <exception cref="MimirorgDuplicateException">Throws if transport already exist</exception>
        /// <remarks>Remember that creating a new transport could be creating a new version of existing transport.
        /// They will have the same first version id, but have different version and id.</remarks>
        public async Task<TransportLibCm> Create(TransportLibAm transportAm)
        {
            if (transportAm == null)
                throw new ArgumentNullException(nameof(transportAm));

            var validation = transportAm.ValidateObject();

            if (!validation.IsValid)
                throw new MimirorgBadRequestException("Transport is not valid.", validation);

            if (await _transportRepository.Exist(transportAm.Id))
                throw new MimirorgDuplicateException($"Transport '{transportAm.Name}' and version '{transportAm.Version}' already exist.");

            transportAm.Version = "1.0";
            var transportDm = _mapper.Map<TransportLibDm>(transportAm);

            transportDm.State = State.Draft;

            await _transportRepository.Create(transportDm);
            _transportRepository.ClearAllChangeTrackers();
            _hookService.HookQueue.Enqueue(CacheKey.Transport);

            return GetLatestVersion(transportDm.Id);
        }

        /// <summary>
        /// Update a Transport if the data is allowed to be changed.
        /// </summary>
        /// <param name="transportAm">The transport to update</param>
        /// <returns>The updated transport</returns>
        /// <exception cref="MimirorgBadRequestException">Throws if the transport does not exist,
        /// if it is not valid or there are not allowed changes.</exception>
        /// <remarks>ParentId to old references will also be updated.</remarks>
        public async Task<TransportLibCm> Update(TransportLibAm transportAm)
        {
            var validation = transportAm.ValidateObject();

            if (!validation.IsValid)
                throw new MimirorgBadRequestException("Transport is not valid.", validation);

            var transportToUpdate = _transportRepository.Get().LatestVersion().FirstOrDefault(x => x.Id == transportAm.Id);

            if (transportToUpdate == null)
            {
                validation = new Validation(new List<string> { nameof(TransportLibAm.Name), nameof(TransportLibAm.Version) },
                    $"Transport with name {transportAm.Name}, aspect {transportAm.Aspect}, Rds Code {transportAm.RdsCode}, id {transportAm.Id} and version {transportAm.Version} does not exist.");

                throw new MimirorgBadRequestException("Transport does not exist. Update is not possible.", validation);
            }

            validation = transportToUpdate.HasIllegalChanges(transportAm);

            if (!validation.IsValid)
                throw new MimirorgBadRequestException(validation.Message, validation);

            var versionStatus = transportToUpdate.CalculateVersionStatus(transportAm);

            if (versionStatus == VersionStatus.NoChange)
                return GetLatestVersion(transportToUpdate.Id);

            transportAm.Version = versionStatus switch
            {
                VersionStatus.Minor => transportToUpdate.Version.IncrementMinorVersion(),
                VersionStatus.Major => transportToUpdate.Version.IncrementMajorVersion(),
                _ => transportToUpdate.Version
            };

            var transportDm = _mapper.Map<TransportLibDm>(transportAm);

            transportDm.FirstVersionId = transportToUpdate.FirstVersionId;
            transportDm.State = State.Draft;

            var transportCm = await _transportRepository.Create(transportDm);
            _transportRepository.ClearAllChangeTrackers();

            await _transportRepository.ChangeParentId(transportAm.Id, transportCm.Id);
            _hookService.HookQueue.Enqueue(CacheKey.Transport);

            return GetLatestVersion(transportCm.Id);
        }

        /// <summary>
        /// Change transport state
        /// </summary>
        /// <param name="id">The transport id that should change the state</param>
        /// <param name="state">The new transport state</param>
        /// <returns>Transport with updated state</returns>
        /// <exception cref="MimirorgNotFoundException">Throws if the transport does not exist on latest version</exception>
        public async Task<TransportLibCm> ChangeState(string id, State state)
        {
            var dm = _transportRepository.Get().LatestVersion().FirstOrDefault(x => x.Id == id);

            if (dm == null)
                throw new MimirorgNotFoundException($"Transport with id {id} not found, or is not latest version");

            await _transportRepository.ChangeState(state, new List<string> { id });
            _hookService.HookQueue.Enqueue(CacheKey.Transport);
            return state == State.Deleted ? null : GetLatestVersion(id);
        }

        /// <summary>
        /// Get transport existing company id
        /// </summary>
        /// <param name="id">The transport id</param>
        /// <returns>Company id for transport</returns>
        public async Task<int> GetCompanyId(string id)
        {
            return await _transportRepository.HasCompany(id);
        }
    }
}