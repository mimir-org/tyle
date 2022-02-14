using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories
{
    public class CollectionRepository : GenericRepository<TypeLibraryDbContext, CollectionLibDm>, ICollectionRepository
    {
        public CollectionRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
