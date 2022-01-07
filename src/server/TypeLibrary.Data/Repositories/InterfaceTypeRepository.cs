using Mb.Models.Abstract;
using Mb.Models.Configurations;
using Mb.Models.Data.TypeEditor;
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
