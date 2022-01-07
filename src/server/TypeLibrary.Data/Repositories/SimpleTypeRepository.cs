using Mb.Models.Abstract;
using Mb.Models.Configurations;
using Mb.Models.Data.TypeEditor;
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
