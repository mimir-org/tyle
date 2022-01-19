using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Models.Data;

namespace TypeLibrary.Data.Repositories
{
    public class UnitRepository : GenericRepository<TypeLibraryDbContext, Unit>, IUnitRepository
    {
        public UnitRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
