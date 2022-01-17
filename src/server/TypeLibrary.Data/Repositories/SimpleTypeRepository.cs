using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Models.Data;

namespace TypeLibrary.Data.Repositories
{
    public class SimpleTypeRepository : GenericRepository<TypeLibraryDbContext, SimpleType>, ISimpleTypeRepository
    {
        public SimpleTypeRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
