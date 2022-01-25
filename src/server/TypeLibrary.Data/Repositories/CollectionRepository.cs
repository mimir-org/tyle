using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Models.Data;

namespace TypeLibrary.Data.Repositories
{
    public class CollectionRepository : GenericRepository<TypeLibraryDbContext, Collection>, ICollectionRepository
    {
        public CollectionRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
