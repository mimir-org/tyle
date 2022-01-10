using TypeLibrary.Models.Abstract;
using TypeLibrary.Models.Data.TypeEditor;
using TypeLibrary.Data.Contracts;

namespace TypeLibrary.Data.Repositories
{
    public class BlobDataRepository : GenericRepository<TypeLibraryDbContext, BlobData>, IBlobDataRepository
    {
        public BlobDataRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
