using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;

using TypeLibrary.Models.Models.Data;

namespace TypeLibrary.Data.Repositories
{
    public class UnitRepository : GenericRepository<TypeLibraryDbContext, UnitDm>, IUnitRepository
    {
        public UnitRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
