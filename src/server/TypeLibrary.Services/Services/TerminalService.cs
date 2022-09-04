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

        public IEnumerable<TerminalLibCm> Get()
        {
            var firstVersionIdsDistinct = _terminalRepository.Get().Select(y => y.FirstVersionId).Distinct().ToList();
            var allTerminals = firstVersionIdsDistinct.Select(GetLatestTerminalVersion).ToList();
            var terminals = allTerminals.Where(x => x.ParentId != null).ToList();
            var topParents = allTerminals.Where(x => x.ParentId == null).OrderBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList();

            var sortedTerminals = terminals.OrderBy(x => topParents
                .FirstOrDefault(y => y.Id == x.ParentId)?.Name, StringComparer.InvariantCultureIgnoreCase)
                .ThenBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList();

            sortedTerminals.AddRange(topParents);

            return _mapper.Map<List<TerminalLibCm>>(sortedTerminals);
        }

        public async Task<TerminalLibCm> Get(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new MimirorgBadRequestException("Can't get terminal. The id is missing value.");

            var terminalDm = await _terminalRepository.Get(id);

            if (terminalDm == null)
                throw new MimirorgNotFoundException($"There is no terminal with id: {id}");

            var latestVersion = await _versionService.GetLatestVersion(terminalDm);

            if (latestVersion != null && terminalDm.Id != latestVersion.Id)
                throw new MimirorgBadRequestException($"The terminal with id {id} and version {terminalDm.Version} is older than latest version {latestVersion.Version}.");

            var terminalLibCm = _mapper.Map<TerminalLibCm>(terminalDm);

            if (terminalLibCm == null)
                throw new MimirorgMappingException("TerminalLibDm", "TerminalLibCm");

            return terminalLibCm;
        }

        public async Task Create(List<TerminalLibAm> terminalAmList, bool createdBySystem = false)
        {
            if (terminalAmList == null || !terminalAmList.Any())
                return;

            var data = _mapper.Map<List<TerminalLibDm>>(terminalAmList);
            var existing = _terminalRepository.Get().ToList();
            var notExisting = data.Where(x => existing.All(y => y.Name != x.Name)).ToList();

            if (!notExisting.Any())
                return;

            foreach (var entity in notExisting)
                entity.CreatedBy = createdBySystem ? _applicationSettings.System : entity.CreatedBy;

            await _terminalRepository.Create(notExisting);
            _terminalRepository.ClearAllChangeTrackers();
        }

        public async Task<TerminalLibCm> Create(TerminalLibAm terminal)
        {
            if (terminal == null)
                throw new MimirorgBadRequestException("Data object can not be null.");

            var validate = terminal.ValidateObject();
            if (!validate.IsValid)
                throw new MimirorgBadRequestException("Terminal is not valid.", validate);

            var existing = await _terminalRepository.Get(terminal.Id);
            if (existing != null)
                throw new MimirorgDuplicateException($"Terminal '{existing.Name}' and version '{existing.Version}' already exist in db.");

            var terminalDm = _mapper.Map<TerminalLibDm>(terminal);

            if (!double.TryParse(terminalDm.Version, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out _))
                throw new MimirorgBadRequestException($"The version number must be of format x.y. Number is invalid: '{terminalDm.Version}'.");

            await _terminalRepository.Create(terminalDm);
            _terminalRepository.ClearAllChangeTrackers();

            _hookService.HookQueue.Enqueue(CacheKey.Terminal);
            return _mapper.Map<TerminalLibCm>(terminalDm);
        }

        public async Task<TerminalLibCm> Update(TerminalLibAm terminal, string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new MimirorgBadRequestException("Can't update a terminal without an id.");

            if (terminal == null)
                throw new MimirorgBadRequestException("Can't update a terminal that is null.");

            var terminalToUpdate = await _terminalRepository.Get(id);

            if (terminalToUpdate?.Id == null)
                throw new MimirorgNotFoundException($"Terminal with id {id} does not exist, update is not possible.");

            if (terminalToUpdate.CreatedBy == _applicationSettings.System)
                throw new MimirorgBadRequestException($"The terminal with id {id} is created by the system and can not be updated.");

            if (terminalToUpdate.Deleted)
                throw new MimirorgBadRequestException($"The terminal with id {id} is deleted and can not be updated.");

            var latestTerminalDm = await _versionService.GetLatestVersion(terminalToUpdate);

            if (latestTerminalDm == null)
                throw new MimirorgBadRequestException($"Latest node version for node with id {id} not found (null).");

            if (string.IsNullOrWhiteSpace(latestTerminalDm.Version))
                throw new MimirorgBadRequestException($"Latest version for node with id {id} has null or empty as version number.");

            var latestTerminalVersion = double.Parse(latestTerminalDm.Version, CultureInfo.InvariantCulture);
            var terminalToUpdateVersion = double.Parse(terminalToUpdate.Version, CultureInfo.InvariantCulture);

            if (latestTerminalVersion > terminalToUpdateVersion)
                throw new MimirorgBadRequestException($"Not allowed to update node with id {terminalToUpdate.Id} and version {terminalToUpdateVersion}. Latest version is node with id {latestTerminalDm.Id} and version {latestTerminalVersion}");

            // Get version
            var validation = latestTerminalDm.HasIllegalChanges(terminal);

            if (!validation.IsValid)
                throw new MimirorgBadRequestException(validation.Message, validation);

            var versionStatus = latestTerminalDm.CalculateVersionStatus(terminal);
            if (versionStatus == VersionStatus.NoChange)
                return await Get(latestTerminalDm.Id);

            terminal.FirstVersionId = latestTerminalDm.FirstVersionId;
            terminal.Version = versionStatus switch
            {
                VersionStatus.Minor => latestTerminalDm.Version.IncrementMinorVersion(),
                VersionStatus.Major => latestTerminalDm.Version.IncrementMajorVersion(),
                _ => latestTerminalDm.Version
            };

            return await Create(terminal);
        }

        public async Task<bool> CompanyIsChanged(string terminalId, int companyId)
        {
            var terminal = await Get(terminalId);

            if (terminal == null)
                throw new MimirorgNotFoundException($"Couldn't find terminal with id: {terminalId}");

            return terminal.CompanyId != companyId;
        }

        #region Private

        private TerminalLibDm GetLatestTerminalVersion(string firstVersionId)
        {
            var existingDmVersions = _terminalRepository.GetVersions(firstVersionId).ToList();

            if (!existingDmVersions.Any())
                throw new MimirorgBadRequestException($"No terminals with 'FirstVersionId' {firstVersionId} found.");

            return existingDmVersions[^1];
        }

        #endregion Private
    }
}