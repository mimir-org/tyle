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
using TypeLibrary.Data.Repositories.Ef;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Services
{
    public class InterfaceService : IInterfaceService
    {
        private readonly IMapper _mapper;
        private readonly IInterfaceRepository _interfaceRepository;
        private readonly ITimedHookService _hookService;

        public InterfaceService(IMapper mapper, IInterfaceRepository interfaceRepository, ITimedHookService hookService)
        {
            _mapper = mapper;
            _interfaceRepository = interfaceRepository;
            _hookService = hookService;
        }

        /// <summary>
        /// Get the latest version of an interface based on given id
        /// </summary>
        /// <param name="id">The id of the interface</param>
        /// <returns>The latest version of the interface of given id</returns>
        /// <exception cref="MimirorgNotFoundException">Throws if there is no interface with the given id, and that interface is at the latest version.</exception>
        public InterfaceLibCm GetLatestVersion(string id)
        {
            var dm = _interfaceRepository.Get().LatestVersion().FirstOrDefault(x => x.Id == id);

            if (dm == null)
                throw new MimirorgNotFoundException($"Interface with id {id} not found.");

            return _mapper.Map<InterfaceLibCm>(dm);
        }

        /// <summary>
        /// Get the latest interface versions
        /// </summary>
        /// <returns>A collection of interface</returns>
        public IEnumerable<InterfaceLibCm> GetLatestVersions()
        {
            var dms = _interfaceRepository.Get()?.LatestVersion()?.OrderBy(x => x.Aspect).ThenBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList();

            if (dms == null)
                throw new MimirorgNotFoundException("No interfaces were found.");

            foreach (var interfaceDm in dms)
                interfaceDm.Children = dms.Where(x => x.ParentId == interfaceDm.Id).ToList();

            return !dms.Any() ? new List<InterfaceLibCm>() : _mapper.Map<List<InterfaceLibCm>>(dms);
        }

        /// <summary>
        /// Create a new interface
        /// </summary>
        /// <param name="interfaceAm">The interface that should be created</param>
        /// <returns>The created interface</returns>
        /// <exception cref="MimirorgBadRequestException">Throws if interface is not valid</exception>
        /// <exception cref="MimirorgDuplicateException">Throws if interface already exist</exception>
        /// <remarks>Remember that creating a new interface could be creating a new version of existing interface.
        /// They will have the same first version id, but have different version and id.</remarks>
        public async Task<InterfaceLibCm> Create(InterfaceLibAm interfaceAm)
        {
            if (interfaceAm == null)
                throw new ArgumentNullException(nameof(interfaceAm));

            var validation = interfaceAm.ValidateObject();

            if (!validation.IsValid)
                throw new MimirorgBadRequestException("Interface is not valid.", validation);

            if (await _interfaceRepository.Exist(interfaceAm.Id))
                throw new MimirorgDuplicateException($"Interface '{interfaceAm.Name}' and version '{interfaceAm.Version}' already exist.");

            interfaceAm.Version = "1.0";
            var interfaceDm = _mapper.Map<InterfaceLibDm>(interfaceAm);

            interfaceDm.State = State.Draft;

            await _interfaceRepository.Create(interfaceDm);
            _interfaceRepository.ClearAllChangeTrackers();
            _hookService.HookQueue.Enqueue(CacheKey.Interface);

            return GetLatestVersion(interfaceDm.Id);
        }

        /// <summary>
        /// Update an Interface if the data is allowed to be changed.
        /// </summary>
        /// <param name="interfaceAm">The interface to update</param>
        /// <returns>The updated interface</returns>
        /// <exception cref="MimirorgBadRequestException">Throws if the interface does not exist,
        /// if it is not valid or there are not allowed changes.</exception>
        /// <remarks>ParentId to old references will also be updated.</remarks>
        public async Task<InterfaceLibCm> Update(InterfaceLibAm interfaceAm)
        {
            var validation = interfaceAm.ValidateObject();

            if (!validation.IsValid)
                throw new MimirorgBadRequestException("Interface is not valid.", validation);

            var interfaceToUpdate = _interfaceRepository.Get().LatestVersion().FirstOrDefault(x => x.Id == interfaceAm.Id);

            if (interfaceToUpdate == null)
            {
                validation = new Validation(new List<string> { nameof(InterfaceLibAm.Name), nameof(InterfaceLibAm.Version) },
                    $"Interface with name {interfaceAm.Name}, aspect {interfaceAm.Aspect}, Rds Code {interfaceAm.RdsCode}, id {interfaceAm.Id} and version {interfaceAm.Version} does not exist.");

                throw new MimirorgBadRequestException("Interface does not exist. Update is not possible.", validation);
            }

            validation = interfaceToUpdate.HasIllegalChanges(interfaceAm);

            if (!validation.IsValid)
                throw new MimirorgBadRequestException(validation.Message, validation);

            var versionStatus = interfaceToUpdate.CalculateVersionStatus(interfaceAm);

            if (versionStatus == VersionStatus.NoChange)
                return GetLatestVersion(interfaceToUpdate.Id);

            interfaceAm.Version = versionStatus switch
            {
                VersionStatus.Minor => interfaceToUpdate.Version.IncrementMinorVersion(),
                VersionStatus.Major => interfaceToUpdate.Version.IncrementMajorVersion(),
                _ => interfaceToUpdate.Version
            };

            var interfaceDm = _mapper.Map<InterfaceLibDm>(interfaceAm);

            interfaceDm.FirstVersionId = interfaceToUpdate.FirstVersionId;
            interfaceDm.State = State.Draft;

            var interfaceCm = await _interfaceRepository.Create(interfaceDm);
            _interfaceRepository.ClearAllChangeTrackers();

            await _interfaceRepository.ChangeParentId(interfaceAm.Id, interfaceCm.Id);
            _hookService.HookQueue.Enqueue(CacheKey.Interface);

            return GetLatestVersion(interfaceCm.Id);
        }

        /// <summary>
        /// Change interface state
        /// </summary>
        /// <param name="id">The interface id that should change the state</param>
        /// <param name="state">The new interface state</param>
        /// <returns>Interface with updated state</returns>
        /// <exception cref="MimirorgNotFoundException">Throws if the interface does not exist on latest version</exception>
        public async Task<InterfaceLibCm> ChangeState(string id, State state)
        {
            var dm = _interfaceRepository.Get().LatestVersion().FirstOrDefault(x => x.Id == id);

            if (dm == null)
                throw new MimirorgNotFoundException($"Interface with id {id} not found, or is not latest version.");

            var dmAllVersions = _interfaceRepository.Get().Where(x => x.FirstVersionId == dm.FirstVersionId).Select(x => x.Id).ToList();

            await _interfaceRepository.ChangeState(state, dmAllVersions);
            _hookService.HookQueue.Enqueue(CacheKey.Interface);
            return state == State.Deleted ? null : GetLatestVersion(id);
        }

        /// <summary>
        /// Get interface existing company id
        /// </summary>
        /// <param name="id">The interface id</param>
        /// <returns>Company id for the interface</returns>
        public async Task<int> GetCompanyId(string id)
        {
            return await _interfaceRepository.HasCompany(id);
        }

    }
}