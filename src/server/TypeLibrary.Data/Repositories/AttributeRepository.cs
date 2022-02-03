using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;

using TypeLibrary.Models.Models.Data;

namespace TypeLibrary.Data.Repositories
{
    public class AttributeRepository : GenericRepository<TypeLibraryDbContext, AttributeLibDm>, IAttributeRepository
    {
        public AttributeRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
