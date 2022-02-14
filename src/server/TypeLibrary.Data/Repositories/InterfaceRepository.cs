using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories
{
    public class InterfaceRepository : GenericRepository<TypeLibraryDbContext, InterfaceLibDm>, IInterfaceRepository
    {
        public InterfaceRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
