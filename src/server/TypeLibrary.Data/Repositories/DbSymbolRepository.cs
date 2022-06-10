using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories
{
    public class DbSymbolRepository : ISymbolRepository
    {
        private readonly IEfSymbolRepository _symbolRepository;

        public DbSymbolRepository(IEfSymbolRepository symbolRepository)
        {
            _symbolRepository = symbolRepository;
        }

        public IEnumerable<SymbolLibDm> Get()
        {
            return _symbolRepository.GetAll().Where(x => !x.Deleted);
        }

        public async Task Create(List<SymbolLibDm> dataDm)
        {
            foreach (var data in dataDm)
                _symbolRepository.Attach(data, EntityState.Added);

            await _symbolRepository.SaveAsync();

            foreach (var data in dataDm)
                _symbolRepository.Detach(data);
        }

        public void ClearAllChangeTrackers()
        {
            _symbolRepository?.Context?.ChangeTracker.Clear();
        }
    }
}