using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using Mimirorg.Authentication.Contracts;
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
        private readonly ApplicationSettings _applicationSettings;
        private readonly ITimedHookService _hookService;
        private readonly IVersionService _versionService;

        public TerminalService(ITerminalRepository terminalRepository, IMapper mapper, IOptions<ApplicationSettings> applicationSettings, ITimedHookService hookService, IVersionService versionService)
        {
            _terminalRepository = terminalRepository;
            _mapper = mapper;
            _hookService = hookService;
            _versionService = versionService;
            _applicationSettings = applicationSettings?.Value;
        }

        /// <summary>
        /// Get the latest version of a terminal based on given id
        /// </summary>
        /// <param name="id">The id of the terminal</param>
        /// <param name="includeDeleted">Include deleted versions</param>
        /// <returns>The latest version of the terminal of given id</returns>
        /// <exception cref="MimirorgNotFoundException">Throws if there is no terminal with the given id, and that terminal is at the latest version.</exception>
        public TerminalLibCm GetLatestVersion(string id, bool includeDeleted = false)
        {
            var terminal = GetLatestVersions(includeDeleted).FirstOrDefault(x => x.Id == id);

            if (terminal == null)
                throw new MimirorgNotFoundException($"There is no terminal with id {id}");

            return terminal;
        }

        /// <summary>
        /// Get the latest terminal versions
        /// </summary>
        /// <param name="includeDeleted">Include deleted versions</param>
        /// <returns>A collection of terminals</returns>
        public IEnumerable<TerminalLibCm> GetLatestVersions(bool includeDeleted = false)
        {
            var terminals = includeDeleted
                ? _terminalRepository.Get().LatestVersion().ToList()
                : _terminalRepository.Get().Where(x => x.State != State.Deleted).LatestVersion().ToList();

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
            await _terminalRepository.Create(dm, State.Draft);
            _terminalRepository.ClearAllChangeTrackers();

            _hookService.HookQueue.Enqueue(CacheKey.Terminal);
            return _mapper.Map<TerminalLibCm>(dm);
        }

        public async Task<TerminalLibCm> Update(TerminalLibAm terminal, bool includeDeleted = false)
        {
            var validation = terminal.ValidateObject();
            if (!validation.IsValid)
                throw new MimirorgBadRequestException("Terminal is not valid.", validation);

            var terminalToUpdate = includeDeleted
                ? _terminalRepository.Get().LatestVersion().FirstOrDefault(x => x.Id == terminal.Id)
                : _terminalRepository.Get().Where(x => x.State != State.Deleted && x.Id == terminal.Id).LatestVersion().FirstOrDefault();

            if (terminalToUpdate == null)
            {
                validation = new Validation(new List<string> { nameof(TerminalLibAm.Name), nameof(TerminalLibAm.Version) },
                    $"Terminal with name {terminal.Name}, id {terminal.Id} and version {terminal.Version} does not exist.");
                throw new MimirorgBadRequestException($"Terminal does not exist. Update is not possible.", validation);
            }

            var oldVersionId = terminal.Id;

            // Get version
            validation = terminalToUpdate.HasIllegalChanges(terminal);

            if (!validation.IsValid)
                throw new MimirorgBadRequestException(validation.Message, validation);

            var versionStatus = terminalToUpdate.CalculateVersionStatus(terminal);
            if (versionStatus == VersionStatus.NoChange)
                return GetLatestVersion(terminalToUpdate.Id);

            terminal.FirstVersionId = terminalToUpdate.FirstVersionId;
            terminal.Version = versionStatus switch
            {
                VersionStatus.Minor => terminalToUpdate.Version.IncrementMinorVersion(),
                VersionStatus.Major => terminalToUpdate.Version.IncrementMajorVersion(),
                _ => terminalToUpdate.Version
            };

            var cm = await Create(terminal, false);
            return GetLatestVersion(cm.Id);
        }

        public async Task<TerminalLibCm> UpdateState(string id, State state)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new MimirorgBadRequestException("Can't update a terminal without an id.");

            var terminalToUpdate = await _terminalRepository.Get(id);

            if (terminalToUpdate?.Id == null)
                throw new MimirorgNotFoundException($"Terminal with id {id} does not exist, update is not possible.");

            if (terminalToUpdate.CreatedBy == _applicationSettings.System)
                throw new MimirorgBadRequestException($"The terminal with id {id} is created by the system and can not be updated.");

            if (terminalToUpdate.State == State.Deleted)
                throw new MimirorgBadRequestException($"The terminal with id {id} is deleted and can not be updated.");

            var latestTerminalDm = await _versionService.GetLatestVersion(terminalToUpdate);

            if (latestTerminalDm == null)
                throw new MimirorgBadRequestException($"Latest terminal version for terminal with id {id} not found (null).");

            if (string.IsNullOrWhiteSpace(latestTerminalDm.Version))
                throw new MimirorgBadRequestException($"Latest version for node with id {id} has null or empty as version number.");

            var latestTerminalVersion = double.Parse(latestTerminalDm.Version, CultureInfo.InvariantCulture);
            var terminalToUpdateVersion = double.Parse(terminalToUpdate.Version, CultureInfo.InvariantCulture);

            if (latestTerminalVersion > terminalToUpdateVersion)
                throw new MimirorgBadRequestException($"Not allowed to update terminal with id {terminalToUpdate.Id} and version {terminalToUpdateVersion}. Latest version is node with id {latestTerminalDm.Id} and version {latestTerminalVersion}");

            await _terminalRepository.UpdateState(id, state);
            _terminalRepository.ClearAllChangeTrackers();

            var cm = GetLatestVersion(id);

            if (cm != null)
                _hookService.HookQueue.Enqueue(CacheKey.Terminal);

            return cm;
        }

        public async Task<bool> Delete(string id)
        {
            try
            {
                var deleted = await _terminalRepository.Remove(id);

                if (deleted)
                    _hookService.HookQueue.Enqueue(CacheKey.Terminal);

                return deleted;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public Task<bool> CompanyIsChanged(string terminalId, int companyId)
        {
            var terminal = GetLatestVersion(terminalId);

            if (terminal == null)
                throw new MimirorgNotFoundException($"Couldn't find terminal with id: {terminalId}");

            return Task.FromResult(terminal.CompanyId != companyId);
        }
    }
}