using Mb.Models.Abstract;
using Mb.Models.Configurations;
using Mb.Models.Data;
using TypeLibrary.Data.Contracts;

namespace TypeLibrary.Data.Repositories
{
    public class PredefinedAttributeRepository : GenericRepository<TypeLibraryDbContext, PredefinedAttribute>, IPredefinedAttributeRepository
    {
        public PredefinedAttributeRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
