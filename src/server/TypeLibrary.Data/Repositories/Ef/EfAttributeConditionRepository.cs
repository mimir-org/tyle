using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories.Ef
{
    public class EfAttributeConditionRepository : GenericRepository<TypeLibraryDbContext, AttributeConditionLibDm>, IEfAttributeConditionRepository
    {
        public EfAttributeConditionRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}