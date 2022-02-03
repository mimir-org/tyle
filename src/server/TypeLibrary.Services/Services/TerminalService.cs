using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TypeLibrary.Data.Contracts;

using TypeLibrary.Models.Models.Application;
using TypeLibrary.Models.Models.Data;
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
        public IEnumerable<TerminalDm> GetTerminals()
        {
            return _terminalTypeRepository.GetAll()
                //.Include(x => x.TerminalCategory)
                .Include(x => x.Attributes)
                .ToList();
        }


        //TODO: Returner en
        /// <summary>
        /// Get all terminals
        /// </summary>
        /// <returns></returns>
        public List<TerminalDm> GetTerminalsByCategory()
        {
            var result = _terminalTypeRepository.GetAll()
                .Include(x => x.Attributes)
                .Include(x => x.Children)
                .ToList();
            
            return result;
        }

        /// <summary>
        /// Create a terminal typeDm
        /// </summary>
        /// <param name="terminalAm"></param>
        /// <returns></returns>
        public async Task<TerminalDm> CreateTerminalType(TerminalAm terminalAm)
        {
            var data = await CreateTerminalTypes(new List<TerminalAm> { terminalAm });
            return data.SingleOrDefault();
        }

        /// <summary>
        /// Create from a list of terminal types
        /// </summary>
        /// <param name="terminalAmList"></param>
        /// <returns></returns>
        public async Task<List<TerminalDm>> CreateTerminalTypes(List<TerminalAm> terminalAmList)
        {
            if (terminalAmList == null || !terminalAmList.Any())
                return new List<TerminalDm>();

            var data = _mapper.Map<List<TerminalDm>>(terminalAmList);
            var existing = _terminalTypeRepository.GetAll().ToList();
            var notExisting = data.Where(x => existing.All(y => y.Name != x.Name)).ToList();

            if (!notExisting.Any())
                return new List<TerminalDm>();

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
