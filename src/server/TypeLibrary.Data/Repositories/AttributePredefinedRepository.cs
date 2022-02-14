using Mimirorg.Common.Abstract;

using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories
{
    public class AttributePredefinedRepository : GenericRepository<TypeLibraryDbContext, AttributePredefinedLibDm>, IAttributePredefinedRepository
    {
        public AttributePredefinedRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
