using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;
using Mimirorg.TypeLibrary.Models.Data;

namespace TypeLibrary.Data.Repositories
{
    public class RdsRepository : GenericRepository<TypeLibraryDbContext, RdsLibDm>, IRdsRepository
    {
        public RdsRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
