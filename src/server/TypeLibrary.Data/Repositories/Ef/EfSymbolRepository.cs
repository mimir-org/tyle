using System.Threading.Tasks;
using Mimirorg.Common.Abstract;
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
    }
}