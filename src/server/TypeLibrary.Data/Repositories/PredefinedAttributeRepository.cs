using Mimirorg.Common.Abstract;

using TypeLibrary.Data.Contracts;
using TypeLibrary.Models.Models.Data;

namespace TypeLibrary.Data.Repositories
{
    public class PredefinedAttributeRepository : GenericRepository<TypeLibraryDbContext, PredefinedAttributeDm>, IPredefinedAttributeRepository
    {
        public PredefinedAttributeRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
