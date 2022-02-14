using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories
{
    public class AttributeAspectRepository : GenericRepository<TypeLibraryDbContext, AttributeAspectLibDm>, IAttributeAspectRepository
    {
        public AttributeAspectRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
