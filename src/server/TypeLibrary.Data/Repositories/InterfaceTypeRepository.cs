using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;

using TypeLibrary.Models.Models.Data;

namespace TypeLibrary.Data.Repositories
{
    public class InterfaceTypeRepository : GenericRepository<TypeLibraryDbContext, InterfaceDm>, IInterfaceTypeRepository
    {
        public InterfaceTypeRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
