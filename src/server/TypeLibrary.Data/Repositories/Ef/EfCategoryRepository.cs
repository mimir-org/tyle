using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories.Ef;

public class EfCategoryRepository : GenericRepository<TypeLibraryDbContext, CategoryLibDm>, IEfCategoryRepository
{
    public EfCategoryRepository(TypeLibraryDbContext dbContext) : base(dbContext)
    {
    }
}