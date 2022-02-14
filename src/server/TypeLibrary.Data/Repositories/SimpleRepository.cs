using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories
{
    public class SimpleRepository : GenericRepository<TypeLibraryDbContext, SimpleLibDm>, ISimpleRepository
    {
        public SimpleRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
