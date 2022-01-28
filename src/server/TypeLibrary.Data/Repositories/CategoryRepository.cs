using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;

using TypeLibrary.Models.Models.Data;

namespace TypeLibrary.Data.Repositories
{
    public class CategoryRepository : GenericRepository<TypeLibraryDbContext, CategoryDm>, ICategoryRepository
    {
        public CategoryRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
