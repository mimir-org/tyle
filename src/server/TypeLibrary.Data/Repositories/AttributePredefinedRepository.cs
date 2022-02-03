using Mimirorg.Common.Abstract;

using TypeLibrary.Data.Contracts;
using TypeLibrary.Models.Models.Data;

namespace TypeLibrary.Data.Repositories
{
    public class AttributePredefinedRepository : GenericRepository<TypeLibraryDbContext, AttributePredefinedDm>, IAttributePredefinedRepository
    {
        public AttributePredefinedRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
