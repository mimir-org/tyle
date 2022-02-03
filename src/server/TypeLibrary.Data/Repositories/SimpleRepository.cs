using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;

using TypeLibrary.Models.Models.Data;

namespace TypeLibrary.Data.Repositories
{
    public class SimpleRepository : GenericRepository<TypeLibraryDbContext, SimpleDm>, ISimpleRepository
    {
        public SimpleRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
