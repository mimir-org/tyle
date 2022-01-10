using Mimirorg.Common.Abstract;
using TypeLibrary.Models.Data.TypeEditor;
using TypeLibrary.Data.Contracts;

namespace TypeLibrary.Data.Repositories
{
    public class RdsRepository : GenericRepository<TypeLibraryDbContext, Rds>, IRdsRepository
    {
        public RdsRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
