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
        private readonly ILogService _logService;

        public TerminalService(ITerminalRepository terminalRepository, IMapper mapper, ITimedHookService hookService, ILogService logService)
        {
            _terminalRepository = terminalRepository;
            _mapper = mapper;
            _hookService = hookService;
            _logService = logService;
        }

        /// <summary>
        /// Get the latest version of a terminal based on given id
        /// </summary>
        /// <param name="id">The id of the terminal</param>
        /// <returns>The latest version of the terminal of given id</returns>
        /// <exception cref="MimirorgNotFoundException">Throws if there is no terminal with the given id, and that terminal is at the latest version.</exception>
        public TerminalLibCm GetLatestVersion(int id)
        {
            var dm = _terminalRepository.Get().LatestVersionsExcludeDeleted().FirstOrDefault(x => x.Id == id);

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
            var dms = _terminalRepository.Get()?.LatestVersionsExcludeDeleted()?.OrderBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList();

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

            terminal.Version = "1.0";
            var dm = _mapper.Map<TerminalLibDm>(terminal);

            dm.State = State.Draft;

            // TODO: This is a temporary fix, since the TS types are not built correctly for nullable ints
            if (dm.ParentId == 0) dm.ParentId = null;

            var createdTerminal = await _terminalRepository.Create(dm);
            _terminalRepository.ClearAllChangeTrackers();
            await _logService.CreateLog(createdTerminal, LogType.State, State.Draft.ToString());
            _hookService.HookQueue.Enqueue(CacheKey.Terminal);

            return GetLatestVersion(createdTerminal.Id);
        }

        /// <summary>
        /// Update a terminal if the data is allowed to be changed.
        /// </summary>
        /// <param name="id">The id of the terminal to update</param>
        /// <param name="terminalAm">The terminal to update</param>
        /// <returns>The updated terminal</returns>
        /// <exception cref="MimirorgBadRequestException">Throws if the terminal does not exist,
        /// if it is not valid or there are not allowed changes.</exception>
        /// <remarks>ParentId to old references will also be updated.</remarks>
        public async Task<TerminalLibCm> Update(int id, TerminalLibAm terminalAm)
        {
            var validation = terminalAm.ValidateObject();

            if (!validation.IsValid)
                throw new MimirorgBadRequestException("Terminal is not valid.", validation);

            var terminalToUpdate = _terminalRepository.Get().LatestVersionsExcludeDeleted().FirstOrDefault(x => x.Id == id);

            if (terminalToUpdate == null)
            {
                validation = new Validation(new List<string> { nameof(TerminalLibAm.Name), nameof(TerminalLibAm.Version) },
                    $"Terminal with name {terminalAm.Name}, id {id} and version {terminalAm.Version} does not exist.");
                throw new MimirorgBadRequestException("Terminal does not exist or is flagged as deleted. Update is not possible.", validation);
            }

            validation = terminalToUpdate.HasIllegalChanges(terminalAm);

            if (!validation.IsValid)
                throw new MimirorgBadRequestException(validation.Message, validation);

            var versionStatus = terminalToUpdate.CalculateVersionStatus(terminalAm);

            if (versionStatus == VersionStatus.NoChange)
                return GetLatestVersion(terminalToUpdate.Id);

            //We need to take into account that there exist a higher version that has state 'Deleted'.
            //Therefore we need to increment minor/major from the latest version, including those with state 'Deleted'.
            terminalToUpdate.Version = _terminalRepository.Get().LatestVersionIncludeDeleted(terminalToUpdate.FirstVersionId).Version;

            terminalAm.Version = versionStatus switch
            {
                VersionStatus.Minor => terminalToUpdate.Version.IncrementMinorVersion(),
                VersionStatus.Major => terminalToUpdate.Version.IncrementMajorVersion(),
                _ => terminalToUpdate.Version
            };

            var dm = _mapper.Map<TerminalLibDm>(terminalAm);

            dm.State = State.Draft;
            dm.FirstVersionId = terminalToUpdate.FirstVersionId;

            var terminalCm = await _terminalRepository.Create(dm);
            _terminalRepository.ClearAllChangeTrackers();
            await _terminalRepository.ChangeParentId(id, terminalCm.Id);
            await _logService.CreateLog(dm, LogType.State, State.Draft.ToString());
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
        public async Task<TerminalLibCm> ChangeState(int id, State state)
        {
            var dm = _terminalRepository.Get().LatestVersionsExcludeDeleted().FirstOrDefault(x => x.Id == id);

            if (dm == null)
                throw new MimirorgNotFoundException($"Terminal with id {id} not found, or is not latest version.");

            await _terminalRepository.ChangeState(state, new List<int> { dm.Id });
            await _logService.CreateLog(dm, LogType.State, state.ToString());
            _hookService.HookQueue.Enqueue(CacheKey.Terminal);

            return state == State.Deleted ? null : GetLatestVersion(id);
        }

        /// <summary>
        /// Get terminal existing company id for terminal by id
        /// </summary>
        /// <param name="id">The terminal id</param>
        /// <returns>Company id for terminal</returns>
        public async Task<int> GetCompanyId(int id)
        {
            return await _terminalRepository.HasCompany(id);
        }
    }
}