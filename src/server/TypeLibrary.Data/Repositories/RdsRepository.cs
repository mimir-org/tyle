using Mb.Models.Abstract;
using Mb.Models.Configurations;
using Mb.Models.Data.TypeEditor;
using TypeLibrary.Data.Contracts;

namespace TypeLibrary.Data.Repositories
{
    public class RdsRepository : GenericRepository<ModelBuilderDbContext, Rds>, IRdsRepository
    {
        public RdsRepository(ModelBuilderDbContext dbContext) : base(dbContext)
        {
        }
    }
}
