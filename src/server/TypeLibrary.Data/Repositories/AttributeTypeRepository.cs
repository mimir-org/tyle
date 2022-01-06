using Mb.Models.Abstract;
using Mb.Models.Configurations;
using Mb.Models.Data.TypeEditor;
using TypeLibrary.Data.Contracts;

namespace TypeLibrary.Data.Repositories
{
    public class AttributeTypeRepository : GenericRepository<ModelBuilderDbContext, AttributeType>, IAttributeTypeRepository
    {
        public AttributeTypeRepository(ModelBuilderDbContext dbContext) : base(dbContext)
        {
        }
    }
}
