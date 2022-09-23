using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mimirorg.Common.Abstract;
using Mimirorg.Common.Enums;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories.Ef
{
    public class EfSymbolRepository : GenericRepository<TypeLibraryDbContext, SymbolLibDm>, IEfSymbolRepository
    {
        public EfSymbolRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<string> GetSymbolDataAsync(string id)
        {
            var symbol = await GetAsync(id);
            return symbol?.Data;
        }

        public IEnumerable<SymbolLibDm> Get()
        {
            return GetAll();
        }

        public async Task Create(List<SymbolLibDm> symbols, State state)
        {
            foreach (var item in symbols)
                item.State = state;

            Attach(symbols, EntityState.Added);
            await SaveAsync();
            Detach(symbols);
        }

        public async Task<SymbolLibDm> Create(SymbolLibDm symbol, State state)
        {
            symbol.State = state;
            Attach(symbol, EntityState.Added);
            await SaveAsync();
            Detach(symbol);
            return symbol;
        }

        public void ClearAllChangeTrackers()
        {
            Context?.ChangeTracker.Clear();
        }

        public void SetUnchanged(ICollection<SymbolLibDm> items)
        {
            Attach(items, EntityState.Unchanged);
        }

        public void SetDetached(ICollection<SymbolLibDm> items)
        {
            Detach(items);
        }
    }
}