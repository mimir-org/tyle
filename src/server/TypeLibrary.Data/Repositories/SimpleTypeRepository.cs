using Mb.Models.Abstract;
using Mb.Models.Configurations;
using Mb.Models.Data.TypeEditor;
using TypeLibrary.Data.Contracts;

namespace TypeLibrary.Data.Repositories
{
    public class SimpleTypeRepository : GenericRepository<ModelBuilderDbContext, SimpleType>, ISimpleTypeRepository
    {
        public SimpleTypeRepository(ModelBuilderDbContext dbContext) : base(dbContext)
        {
        }
    }
}
