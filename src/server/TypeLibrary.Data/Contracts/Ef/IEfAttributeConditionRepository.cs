using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts.Ef
{
    public interface IEfAttributeConditionRepository : IGenericRepository<TypeLibraryDbContext, AttributeConditionLibDm>
    {
    }
}