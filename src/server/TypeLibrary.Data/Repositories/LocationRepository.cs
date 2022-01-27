using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;

using TypeLibrary.Models.Models.Data;

namespace TypeLibrary.Data.Repositories
{
    public class LocationRepository : GenericRepository<TypeLibraryDbContext, LocationDm>, ILocationRepository
    {
        public LocationRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
