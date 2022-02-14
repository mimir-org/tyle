using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories
{
    public class BlobRepository : GenericRepository<TypeLibraryDbContext, BlobLibDm>, IBlobDataRepository
    {
        public BlobRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
