using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Models.Data;

namespace TypeLibrary.Data.Repositories
{
    public class AttributeTypeRepository : GenericRepository<TypeLibraryDbContext, AttributeType>, IAttributeTypeRepository
    {
        public AttributeTypeRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
