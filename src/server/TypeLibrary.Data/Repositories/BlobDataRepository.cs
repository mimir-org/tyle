using Mb.Models.Abstract;
using Mb.Models.Configurations;
using Mb.Models.Data.TypeEditor;
using TypeLibrary.Data.Contracts;

namespace TypeLibrary.Data.Repositories
{
    public class BlobDataRepository : GenericRepository<ModelBuilderDbContext, BlobData>, IBlobDataRepository
    {
        public BlobDataRepository(ModelBuilderDbContext dbContext) : base(dbContext)
        {
        }
    }
}
