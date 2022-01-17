using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Models.Data;

namespace TypeLibrary.Data.Repositories
{
    public class InterfaceTypeRepository : GenericRepository<TypeLibraryDbContext, InterfaceType>, IInterfaceTypeRepository
    {
        public InterfaceTypeRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
