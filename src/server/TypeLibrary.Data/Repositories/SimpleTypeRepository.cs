using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;

using TypeLibrary.Models.Models.Data;

namespace TypeLibrary.Data.Repositories
{
    public class SimpleTypeRepository : GenericRepository<TypeLibraryDbContext, SimpleDm>, ISimpleTypeRepository
    {
        public SimpleTypeRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
