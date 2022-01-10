using TypeLibrary.Models.Abstract;
using TypeLibrary.Models.Configurations;
using TypeLibrary.Models.Data.TypeEditor;
using TypeLibrary.Data.Contracts;

namespace TypeLibrary.Data.Repositories
{
    public class AttributeTypeRepository : GenericRepository<TypeLibraryDbContext, AttributeType>, IAttributeTypeRepository
    {
        public AttributeTypeRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
