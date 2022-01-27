using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Models.Data;

namespace TypeLibrary.Data.Repositories
{
    public class AttributeRepository : GenericRepository<TypeLibraryDbContext, Attribute>, IAttributeRepository
    {
        public AttributeRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
