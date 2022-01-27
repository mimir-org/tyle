using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;

using TypeLibrary.Models.Models.Data;

namespace TypeLibrary.Data.Repositories
{
    public class BlobDataRepository : GenericRepository<TypeLibraryDbContext, BlobDm>, IBlobDataRepository
    {
        public BlobDataRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
