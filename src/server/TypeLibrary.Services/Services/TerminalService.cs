using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using Mimirorg.Common.Exceptions;
using Mimirorg.Common.Models;
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

        public TerminalService(ITerminalRepository terminalRepository, IMapper mapper, IOptions<ApplicationSettings> applicationSettings)
        {
            _terminalRepository = terminalRepository;
            _mapper = mapper;
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