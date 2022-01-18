using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Models.Data;

namespace TypeLibrary.Data.Repositories
{
    public class ConditionRepository : GenericRepository<TypeLibraryDbContext, Condition>, IConditionRepository
    {
        public ConditionRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
