using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Models.Data;

namespace TypeLibrary.Data.Repositories
{
    public class BlobDataRepository : GenericRepository<TypeLibraryDbContext, BlobData>, IBlobDataRepository
    {
        public BlobDataRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
