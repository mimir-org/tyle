using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories
{
    public class DbTerminalRepository : ITerminalRepository
    {
        private readonly IEfTerminalRepository _terminalRepository;
        private readonly IEfAttributeRepository _attributeRepository;

        public DbTerminalRepository(IEfTerminalRepository terminalRepository, IEfAttributeRepository attributeRepository)
        {
            _terminalRepository = terminalRepository;
            _attributeRepository = attributeRepository;
        }

        public IEnumerable<TerminalLibDm> Get()
        {
            return _terminalRepository.GetAll().Where(x => !x.Deleted);
        }

        public async Task Create(List<TerminalLibDm> dataDm)
        {
            foreach (var entity in dataDm)
            {
                foreach (var entityAttribute in entity.Attributes)
                    _attributeRepository.Attach(entityAttribute, EntityState.Unchanged);

                await _terminalRepository.CreateAsync(entity);

                foreach (var entityAttribute in entity.Attributes)
                    _attributeRepository.Detach(entityAttribute);
            }

            await _terminalRepository.SaveAsync();
        }

        public IEnumerable<TerminalLibDm> GetVersions(string firstVersionId)
        {
            return _terminalRepository.GetAll()
                .Where(x => x.FirstVersionId == firstVersionId && !x.Deleted).Include(x => x.Parent).Include(x => x.Attributes).ToList()
                .OrderBy(x => double.Parse(x.Version, CultureInfo.InvariantCulture)).ToList();
        }

        public void ClearAllChangeTrackers()
        {
            _terminalRepository?.Context?.ChangeTracker.Clear();
        }
    }
}