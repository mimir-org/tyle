using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories
{
    public class AttributeConditionRepository : GenericRepository<TypeLibraryDbContext, AttributeConditionLibDm>, IConditionRepository
    {
        public AttributeConditionRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
