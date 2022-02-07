using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;
using Mimirorg.TypeLibrary.Models.Data;

namespace TypeLibrary.Data.Repositories
{
    public class UnitRepository : GenericRepository<TypeLibraryDbContext, UnitLibDm>, IUnitRepository
    {
        public UnitRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
