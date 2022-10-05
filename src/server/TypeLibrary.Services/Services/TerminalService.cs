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
    public class TerminalService : ITerminalService
    {
        private readonly ITerminalRepository _terminalRepository;
        private readonly IMapper _mapper;
        private readonly ITimedHookService _hookService;

        public TerminalService(ITerminalRepository terminalRepository, IMapper mapper, ITimedHookService hookService)
        {
            _terminalRepository = terminalRepository;
            _mapper = mapper;
            _hookService = hookService;
        }

        /// <summary>
        /// Get the latest version of a terminal based on given id
        /// </summary>
        /// <param name="id">The id of the terminal</param>
        /// <returns>The latest version of the terminal of given id</returns>
        /// <exception cref="MimirorgNotFoundException">Throws if there is no terminal with the given id, and that terminal is at the latest version.</exception>
        public TerminalLibCm GetLatestVersion(string id)
        {
            var dm = _terminalRepository.Get().LatestVersion().FirstOrDefault(x => x.Id == id);

            if (dm == null)
                throw new MimirorgNotFoundException($"Terminal with id {id} not found.");

            return _mapper.Map<TerminalLibCm>(dm);
        }

        /// <summary>
        /// Get the latest terminal versions
        /// </summary>
        /// <returns>A collection of terminals</returns>
        public IEnumerable<TerminalLibCm> GetLatestVersions()
        {
            var dms = _terminalRepository.Get()?.LatestVersion()?.OrderBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList();

            if (dms == null)
                throw new MimirorgNotFoundException("No terminals were found.");

            foreach (var terminal in dms)
                terminal.Children = dms.Where(x => x.ParentId == terminal.Id).ToList();

            return !dms.Any() ? new List<TerminalLibCm>() : _mapper.Map<List<TerminalLibCm>>(dms);
        }

        /// <summary>
        /// Create a new terminal
        /// </summary>
        /// <param name="terminal">The terminal that should be created</param>
        /// <returns></returns>
        /// <exception cref="MimirorgBadRequestException">Throws if terminal is not valid</exception>
        /// <exception cref="MimirorgDuplicateException">Throws if terminal already exist</exception>
        /// <remarks>Remember that creating a new terminal could be creating a new version of existing terminal.
        /// They will have the same first version id, but have different version and id.</remarks>
        public async Task<TerminalLibCm> Create(TerminalLibAm terminal)
        {
            if (terminal == null)
                throw new ArgumentNullException(nameof(terminal));

            var validation = terminal.ValidateObject();
            if (!validation.IsValid)
                throw new MimirorgBadRequestException("Terminal is not valid.", validation);

            if (await _terminalRepository.Exist(terminal.Id))
                throw new MimirorgDuplicateException($"Terminal '{terminal.Name}' and version '{terminal.Version}' already exist.");

            terminal.Version = "1.0";
            var dm = _mapper.Map<TerminalLibDm>(terminal);

            dm.State = State.Draft;
            dm.FirstVersionId = dm.Id;

            await _terminalRepository.Create(dm);
            _terminalRepository.ClearAllChangeTrackers();
            _hookService.HookQueue.Enqueue(CacheKey.Terminal);

            return GetLatestVersion(dm.Id);
        }

        /// <summary>
        /// Update a terminal if the data is allowed to be changed.
        /// </summary>
        /// <param name="terminalAm">The terminal to update</param>
        /// <returns>The updated terminal</returns>
        /// <exception cref="MimirorgBadRequestException">Throws if the terminal does not exist,
        /// if it is not valid or there are not allowed changes.</exception>
        /// <remarks>ParentId to old references will also be updated.</remarks>
        public async Task<TerminalLibCm> Update(TerminalLibAm terminalAm)
        {
            var validation = terminalAm.ValidateObject();
            if (!validation.IsValid)
                throw new MimirorgBadRequestException("Terminal is not valid.", validation);

            var terminalToUpdate = _terminalRepository.Get()
                .LatestVersion()
                .FirstOrDefault(x => x.Id == terminalAm.Id);

            if (terminalToUpdate == null)
            {
                validation = new Validation(new List<string> { nameof(TerminalLibAm.Name), nameof(TerminalLibAm.Version) },
                    $"Terminal with name {terminalAm.Name}, id {terminalAm.Id} and version {terminalAm.Version} does not exist.");
                throw new MimirorgBadRequestException("Terminal does not exist. Update is not possible.", validation);
            }

            // Get version
            validation = terminalToUpdate.HasIllegalChanges(terminalAm);

            if (!validation.IsValid)
                throw new MimirorgBadRequestException(validation.Message, validation);

            var versionStatus = terminalToUpdate.CalculateVersionStatus(terminalAm);
            if (versionStatus == VersionStatus.NoChange)
                return GetLatestVersion(terminalToUpdate.Id);

            terminalAm.Version = versionStatus switch
            {
                VersionStatus.Minor => terminalToUpdate.Version.IncrementMinorVersion(),
                VersionStatus.Major => terminalToUpdate.Version.IncrementMajorVersion(),
                _ => terminalToUpdate.Version
            };

            var terminalDm = _mapper.Map<TerminalLibDm>(terminalAm);

            terminalDm.State = State.Draft;
            terminalDm.FirstVersionId = terminalToUpdate.FirstVersionId;
            
            var terminalCm = await _terminalRepository.Create(terminalDm);
            _terminalRepository.ClearAllChangeTrackers();

            await _terminalRepository.ChangeParentId(terminalAm.Id, terminalCm.Id);
            _hookService.HookQueue.Enqueue(CacheKey.Terminal);

            return GetLatestVersion(terminalCm.Id);
        }

        /// <summary>
        /// Change terminal state
        /// </summary>
        /// <param name="id">The terminal id that should change the state</param>
        /// <param name="state">The new terminal state</param>
        /// <returns>Terminal with updated state</returns>
        /// <exception cref="MimirorgNotFoundException">Throws if the terminal does not exist on latest version</exception>
        public async Task<TerminalLibCm> ChangeState(string id, State state)
        {
            var dm = _terminalRepository.Get().LatestVersion().FirstOrDefault(x => x.Id == id);

            if (dm == null)
                throw new MimirorgNotFoundException($"Terminal with id {id} not found, or is not latest version.");

            var dmAllVersions = _terminalRepository.Get().Where(x => x.FirstVersionId == dm.FirstVersionId).Select(x => x.Id).ToList();

            await _terminalRepository.ChangeState(state, dmAllVersions);
            _hookService.HookQueue.Enqueue(CacheKey.Terminal);
            return state == State.Deleted ? null : GetLatestVersion(id);
        }

        /// <summary>
        /// Get terminal existing company id for terminal by id
        /// </summary>
        /// <param name="id">The terminal id</param>
        /// <returns>Company id for terminal</returns>
        public async Task<int> GetCompanyId(string id)
        {
            return await _terminalRepository.HasCompany(id);
        }
    }
}