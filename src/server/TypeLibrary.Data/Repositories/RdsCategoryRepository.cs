using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;

using TypeLibrary.Models.Models.Data;

namespace TypeLibrary.Data.Repositories
{
    public class RdsCategoryRepository : GenericRepository<TypeLibraryDbContext, RdsCategoryLibDm>, IRdsCategoryRepository
    {
        public RdsCategoryRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
