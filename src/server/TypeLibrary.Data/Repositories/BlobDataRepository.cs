using Mb.Models.Abstract;
using Mb.Models.Configurations;
using Mb.Models.Data.TypeEditor;
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
