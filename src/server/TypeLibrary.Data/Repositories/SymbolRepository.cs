using System.Threading.Tasks;
using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories
{
    public class SymbolRepository : GenericRepository<TypeLibraryDbContext, SymbolLibDm>, ISymbolRepository
    {
        public SymbolRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<string> GetSymbolDataAsync(string id)
        {
            var symbol = await GetAsync(id);
            return symbol?.Data;
        }
    }
}
