using Mb.Models.Abstract;
using Mb.Models.Configurations;
using Mb.Models.Data;
using TypeLibrary.Data.Contracts;

namespace TypeLibrary.Data.Repositories
{
    public class PredefinedAttributeRepository : GenericRepository<ModelBuilderDbContext, PredefinedAttribute>, IPredefinedAttributeRepository
    {
        public PredefinedAttributeRepository(ModelBuilderDbContext dbContext) : base(dbContext)
        {
        }
    }
}
