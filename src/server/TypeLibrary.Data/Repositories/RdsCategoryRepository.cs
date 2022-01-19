using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Models.Data;

namespace TypeLibrary.Data.Repositories
{
    public class RdsCategoryRepository : GenericRepository<TypeLibraryDbContext, RdsCategory>, IRdsCategoryRepository
    {
        public RdsCategoryRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
