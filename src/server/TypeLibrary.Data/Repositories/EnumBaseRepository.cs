using Mb.Models.Abstract;
using Mb.Models.Configurations;
using Mb.Models.Data.Enums;
using TypeLibrary.Data.Contracts;

namespace TypeLibrary.Data.Repositories
{
    public class EnumBaseRepository: GenericRepository<ModelBuilderDbContext, EnumBase>, IEnumBaseRepository
    {
        public EnumBaseRepository(ModelBuilderDbContext dbContext) : base(dbContext)
        {
        }
    }
}
