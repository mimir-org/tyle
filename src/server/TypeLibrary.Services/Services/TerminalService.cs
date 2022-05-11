using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Mimirorg.Common.Models;
using TypeLibrary.Data.Contracts;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Models;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Services
{
    public class TerminalService : ITerminalService
    {
        private readonly ITerminalRepository _terminalTypeRepository;
        private readonly IAttributeRepository _attributeRepository;
        private readonly IMapper _mapper;
        private readonly ApplicationSettings _applicationSettings;

        public TerminalService(ITerminalRepository terminalTypeRepository, IAttributeRepository attributeRepository, IMapper mapper, IOptions<ApplicationSettings> applicationSettings)
        {
            _terminalTypeRepository = terminalTypeRepository;
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
            var allTerminals = _terminalTypeRepository.GetAll().Where(x => !x.Deleted).Include(x => x.Parent).Include(x => x.Attributes).ToList();
            var terminals = allTerminals.Where(x => x.ParentId != null).ToList();
            var topParents = allTerminals.Where(x => x.ParentId == null).OrderBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList();

            var sortedTerminals = terminals.OrderBy(x => topParents
                .FirstOrDefault(y => y.Id == x.ParentId)?.Name, StringComparer.InvariantCultureIgnoreCase)
                .ThenBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList();

            sortedTerminals.AddRange(topParents);

            return _mapper.Map<List<TerminalLibCm>>(sortedTerminals);
        }

        /// <summary>
        /// Create a terminal typeDm
        /// </summary>
        /// <param name="terminalAm"></param>
        /// <returns></returns>
        public async Task<TerminalLibCm> CreateTerminal(TerminalLibAm terminalAm)
        {
            var data = await CreateTerminals(new List<TerminalLibAm> { terminalAm });
            return data.SingleOrDefault();
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
            var existing = _terminalTypeRepository.GetAll().ToList();
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

                await _terminalTypeRepository.CreateAsync(entity);
                await _terminalTypeRepository.SaveAsync();

                foreach (var entityAttribute in entity.Attributes)
                {
                    _attributeRepository.Detach(entityAttribute);
                }
            }

            return _mapper.Map<List<TerminalLibCm>>(data);
        }
    }
}
