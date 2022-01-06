using Mb.Models.Abstract;
using Mb.Models.Configurations;
using Mb.Models.Data.TypeEditor;
using TypeLibrary.Data.Contracts;

namespace TypeLibrary.Data.Repositories
{
    public class InterfaceTypeRepository : GenericRepository<ModelBuilderDbContext, InterfaceType>, IInterfaceTypeRepository
    {
        public InterfaceTypeRepository(ModelBuilderDbContext dbContext) : base(dbContext)
        {
        }
    }
}
