using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Mimirorg.Common.Exceptions;
using Mimirorg.Common.Models;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Services
{
    public class TerminalService : ITerminalService
    {
        private readonly IEfTerminalRepository _terminalRepository;
        private readonly IEfAttributeRepository _attributeRepository;
        private readonly IMapper _mapper;
        private readonly ApplicationSettings _applicationSettings;

        public TerminalService(IEfTerminalRepository terminalRepository, IEfAttributeRepository attributeRepository, IMapper mapper, IOptions<ApplicationSettings> applicationSettings)
        {
            _terminalRepository = terminalRepository;
            _attributeRepository = attributeRepository;
            _mapper = mapper;
            _applicationSettings = applicationSettings?.Value;
        }

        /// <summary>
        /// Get all terminals
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TerminalLibCm> GetTerminals()
        {
            var firstVersionIdsDistinct = _terminalRepository.GetAll().Where(x => !x.Deleted).Select(y => y.FirstVersionId).Distinct().ToList();
            var allTerminals = firstVersionIdsDistinct.Select(GetLatestTerminalVersion).ToList();
            var terminals = allTerminals.Where(x => x.ParentId != null).ToList();
            var topParents = allTerminals.Where(x => x.ParentId == null).OrderBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList();

            var sortedTerminals = terminals.OrderBy(x => topParents
                .FirstOrDefault(y => y.Id == x.ParentId)?.Name, StringComparer.InvariantCultureIgnoreCase)
                .ThenBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList();

            sortedTerminals.AddRange(topParents);

            return _mapper.Map<List<TerminalLibCm>>(sortedTerminals);
        }

        /// <summary>
        /// Create from a list of terminal types
        /// </summary>
        /// <param name="terminalAmList"></param>
        /// <param name="createdBySystem"></param>
        /// <returns></returns>
        public async Task<List<TerminalLibCm>> CreateTerminals(List<TerminalLibAm> terminalAmList, bool createdBySystem = false)
        {
            if (terminalAmList == null || !terminalAmList.Any())
                return new List<TerminalLibCm>();

            var data = _mapper.Map<List<TerminalLibDm>>(terminalAmList);
            var existing = _terminalRepository.GetAll().Where(x => !x.Deleted).ToList();
            var notExisting = data.Where(x => existing.All(y => y.Name != x.Name)).ToList();

            if (!notExisting.Any())
                return new List<TerminalLibCm>();

            foreach (var entity in notExisting)
            {
                foreach (var entityAttribute in entity.Attributes)
                {
                    _attributeRepository.Attach(entityAttribute, EntityState.Unchanged);
                }

                entity.CreatedBy = createdBySystem ? _applicationSettings.System : entity.CreatedBy;

                await _terminalRepository.CreateAsync(entity);
                await _terminalRepository.SaveAsync();

                foreach (var entityAttribute in entity.Attributes)
                {
                    _attributeRepository.Detach(entityAttribute);
                }
            }

            return _mapper.Map<List<TerminalLibCm>>(data);
        }

        #region Private

        private TerminalLibDm GetLatestTerminalVersion(string firstVersionId)
        {
            var existingDmVersions = _terminalRepository.GetAll()
                .Where(x => x.FirstVersionId == firstVersionId && !x.Deleted).Include(x => x.Parent).Include(x => x.Attributes).ToList()
                .OrderBy(x => double.Parse(x.Version, CultureInfo.InvariantCulture)).ToList();

            if (!existingDmVersions.Any())
                throw new MimirorgBadRequestException($"No nodes with 'FirstVersionId' {firstVersionId} found.");

            return existingDmVersions[^1];
        }

        #endregion Private
    }
}
