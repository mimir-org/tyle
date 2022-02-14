using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TypeLibrary.Data.Contracts;
using Mimirorg.TypeLibrary.Models.Application;
using TypeLibrary.Data.Models;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Services
{
    public class TerminalService : ITerminalService
    {
        private readonly ITerminalRepository _terminalTypeRepository;
        private readonly IAttributeRepository _attributeRepository;
        private readonly IMapper _mapper;

        public TerminalService(ITerminalRepository terminalTypeRepository, IAttributeRepository attributeRepository, IMapper mapper)
        {
            _terminalTypeRepository = terminalTypeRepository;
            _attributeRepository = attributeRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all terminals
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TerminalLibDm> GetTerminals()
        {
            return _terminalTypeRepository.GetAll()
                .Where(x => string.IsNullOrWhiteSpace(x.ParentId))
                .Include(x => x.Children)
                .Include(x => x.Attributes)
                .ToList();
        }

        /// <summary>
        /// Create a terminal typeDm
        /// </summary>
        /// <param name="terminalAm"></param>
        /// <returns></returns>
        public async Task<TerminalLibDm> CreateTerminal(TerminalLibAm terminalAm)
        {
            var data = await CreateTerminals(new List<TerminalLibAm> { terminalAm });
            return data.SingleOrDefault();
        }

        /// <summary>
        /// Create from a list of terminal types
        /// </summary>
        /// <param name="terminalAmList"></param>
        /// <returns></returns>
        public async Task<List<TerminalLibDm>> CreateTerminals(List<TerminalLibAm> terminalAmList)
        {
            if (terminalAmList == null || !terminalAmList.Any())
                return new List<TerminalLibDm>();

            var data = _mapper.Map<List<TerminalLibDm>>(terminalAmList);
            var existing = _terminalTypeRepository.GetAll().ToList();
            var notExisting = data.Where(x => existing.All(y => y.Name != x.Name)).ToList();

            if (!notExisting.Any())
                return new List<TerminalLibDm>();

            foreach (var entity in notExisting)
            {
                foreach (var entityAttribute in entity.Attributes)
                {
                    _attributeRepository.Attach(entityAttribute, EntityState.Unchanged);
                }

                await _terminalTypeRepository.CreateAsync(entity);
                await _terminalTypeRepository.SaveAsync();

                foreach (var entityAttribute in entity.Attributes)
                {
                    _attributeRepository.Detach(entityAttribute);
                }
            }

            return data;
        }
    }
}
