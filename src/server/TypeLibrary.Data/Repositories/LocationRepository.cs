using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Models.Data;

namespace TypeLibrary.Data.Repositories
{
    public class LocationRepository : GenericRepository<TypeLibraryDbContext, Location>, ILocationRepository
    {
        public LocationRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
