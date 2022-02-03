using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;

using TypeLibrary.Models.Models.Data;

namespace TypeLibrary.Data.Repositories
{
    public class InterfaceRepository : GenericRepository<TypeLibraryDbContext, InterfaceDm>, IInterfaceRepository
    {
        public InterfaceRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
