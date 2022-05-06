using System.Threading.Tasks;
using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories
{
    public class BlobRepository : GenericRepository<TypeLibraryDbContext, BlobLibDm>, IBlobRepository
    {
        public BlobRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<string> GetBlobDataAsync(string id)
        {
            var blob = await GetAsync(id);
            return blob?.Data;
        }
    }
}
