using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Models.Application;

namespace TypeLibrary.Services.Contracts
{
    public interface IAttributeConditionService
    {
        Task<IEnumerable<AttributeConditionLibAm>> GetAttributeConditions();
        Task<AttributeConditionLibAm> UpdateAttributeCondition(AttributeConditionLibAm dataAm);
        Task<AttributeConditionLibAm> CreateAttributeCondition(AttributeConditionLibAm dataAm);
        Task CreateAttributeConditions(List<AttributeConditionLibAm> dataAm);
    }
}