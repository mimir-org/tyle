using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;
using Mimirorg.TypeLibrary.Models.Data;

namespace TypeLibrary.Data.Repositories
{
    public class AttributeTypeRepository : GenericRepository<TypeLibraryDbContext, AttributeTypeLibDm>, IAttributeTypeRepository
    {
        public AttributeTypeRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
