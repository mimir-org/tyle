using TypeLibrary.Models.Abstract;
using TypeLibrary.Models.Configurations;
using TypeLibrary.Models.Data.TypeEditor;
using TypeLibrary.Data.Contracts;

namespace TypeLibrary.Data.Repositories
{
    public class SimpleTypeRepository : GenericRepository<TypeLibraryDbContext, SimpleType>, ISimpleTypeRepository
    {
        public SimpleTypeRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
