using TypeLibrary.Models.Abstract;
using TypeLibrary.Models.Data.TypeEditor;
using TypeLibrary.Data.Contracts;

namespace TypeLibrary.Data.Repositories
{
    public class InterfaceTypeRepository : GenericRepository<TypeLibraryDbContext, InterfaceType>, IInterfaceTypeRepository
    {
        public InterfaceTypeRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
