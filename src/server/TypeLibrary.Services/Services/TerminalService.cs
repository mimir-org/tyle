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
            var terminal = GetLatestVersions().FirstOrDefault(x => x.Id == id);

            if (terminal == null)
                throw new MimirorgNotFoundException($"There is no terminal with id {id}");

            return terminal;
        }

        /// <summary>
        /// Get the latest terminal versions
        /// </summary>
        /// <returns>A collection of terminals</returns>
        public IEnumerable<TerminalLibCm> GetLatestVersions()
        {
            var terminals = _terminalRepository.Get().LatestVersion().ToList();

            terminals = terminals.OrderBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList();

            foreach (var terminal in terminals)
                terminal.Children = terminals.Where(x => x.ParentId == terminal.Id).ToList();

            return !terminals.Any() ? new List<TerminalLibCm>() : _mapper.Map<List<TerminalLibCm>>(terminals);
        }

        /// <summary>
        /// Create a new terminal
        /// </summary>
        /// <param name="terminal">The terminal that should be created</param>
        /// <param name="resetVersion">Would you reset version and first version id?</param>
        /// <returns></returns>
        /// <exception cref="MimirorgBadRequestException">Throws if terminal is not valid</exception>
        /// <exception cref="MimirorgDuplicateException">Throws if terminal already exist</exception>
        /// <remarks>Remember that creating a new terminal could be creating a new version of existing terminal.
        /// They will have the same first version id, but have different version and id.</remarks>
        public async Task<TerminalLibCm> Create(TerminalLibAm terminal, bool resetVersion)
        {
            if (terminal == null)
                throw new ArgumentNullException(nameof(terminal));

            var validation = terminal.ValidateObject();
            if (!validation.IsValid)
                throw new MimirorgBadRequestException("Terminal is not valid.", validation);

            // Version is included in generating id. It must run before check of already exist. 
            if (resetVersion)
            {
                terminal.FirstVersionId = terminal.Id;
                terminal.Version = "1.0";
            }

            if (await _terminalRepository.Exist(terminal.Id))
                throw new MimirorgDuplicateException($"Terminal '{terminal.Name}' and version '{terminal.Version}' already exist.");

            var dm = _mapper.Map<TerminalLibDm>(terminal);
            dm.State = State.Draft;
            
            await _terminalRepository.Create(dm);
            _terminalRepository.ClearAllChangeTrackers();

            _hookService.HookQueue.Enqueue(CacheKey.Terminal);
            return _mapper.Map<TerminalLibCm>(dm);
        }

        /// <summary>
        /// Update a terminal if the data is allowed to be changed.
        /// </summary>
        /// <param name="terminal">The terminal to update</param>
        /// <returns>The updated terminal</returns>
        /// <exception cref="MimirorgBadRequestException">Throws if the terminal does not exist,
        /// if it is not valid or there are not allowed changes.</exception>
        /// <remarks>ParentId to old references will also be updated.</remarks>
        public async Task<TerminalLibCm> Update(TerminalLibAm terminal)
        {
            var validation = terminal.ValidateObject();
            if (!validation.IsValid)
                throw new MimirorgBadRequestException("Terminal is not valid.", validation);

            var terminalToUpdate = _terminalRepository.Get()
                .LatestVersion()
                .FirstOrDefault(x => x.Id == terminal.Id);

            if (terminalToUpdate == null)
            {
                validation = new Validation(new List<string> { nameof(TerminalLibAm.Name), nameof(TerminalLibAm.Version) },
                    $"Terminal with name {terminal.Name}, id {terminal.Id} and version {terminal.Version} does not exist.");
                throw new MimirorgBadRequestException("Terminal does not exist. Update is not possible.", validation);
            }

            // Get version
            validation = terminalToUpdate.HasIllegalChanges(terminal);

            if (!validation.IsValid)
                throw new MimirorgBadRequestException(validation.Message, validation);

            var versionStatus = terminalToUpdate.CalculateVersionStatus(terminal);
            if (versionStatus == VersionStatus.NoChange)
                return GetLatestVersion(terminalToUpdate.Id);

            var oldId = terminal.Id;

            terminal.FirstVersionId = terminalToUpdate.FirstVersionId;
            terminal.Version = versionStatus switch
            {
                VersionStatus.Minor => terminalToUpdate.Version.IncrementMinorVersion(),
                VersionStatus.Major => terminalToUpdate.Version.IncrementMajorVersion(),
                _ => terminalToUpdate.Version
            };

            var cm = await Create(terminal, false);
            await _terminalRepository.ChangeParentId(oldId, cm.Id);
            _hookService.HookQueue.Enqueue(CacheKey.Terminal);
            return GetLatestVersion(cm.Id);
        }

        /// <summary>
        /// Change terminal state
        /// </summary>
        /// <param name="id">The terminal id that should change the state</param>
        /// <param name="state">The new terminal state</param>
        /// <returns>Terminal with updated state</returns>
        /// <exception cref="MimirorgNotFoundException">Throws if the terminal does not exist on latest version</exception>
        public async Task<TerminalLibCm> UpdateState(string id, State state)
        {
            var terminalToUpdate = _terminalRepository.Get()
                .LatestVersion()
                .FirstOrDefault(x => x.Id == id);

            if (terminalToUpdate == null)
                throw new MimirorgNotFoundException($"Terminal with id {id} does not exist, update is not possible.");

            await _terminalRepository.ChangeState(state, new List<string> {id});
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