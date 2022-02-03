using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;

using TypeLibrary.Models.Models.Data;

namespace TypeLibrary.Data.Repositories
{
    public class ConditionRepository : GenericRepository<TypeLibraryDbContext, ConditionLibDm>, IConditionRepository
    {
        public ConditionRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
