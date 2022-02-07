using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;
using Mimirorg.TypeLibrary.Models.Data;

namespace TypeLibrary.Data.Repositories
{
    public class InterfaceRepository : GenericRepository<TypeLibraryDbContext, InterfaceLibDm>, IInterfaceRepository
    {
        public InterfaceRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
