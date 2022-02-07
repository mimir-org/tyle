using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;
using Mimirorg.TypeLibrary.Models.Data;

namespace TypeLibrary.Data.Repositories
{
    public class BlobDataRepository : GenericRepository<TypeLibraryDbContext, BlobLibDm>, IBlobDataRepository
    {
        public BlobDataRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
