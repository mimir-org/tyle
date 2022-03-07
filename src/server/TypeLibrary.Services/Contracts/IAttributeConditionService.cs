using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;

namespace TypeLibrary.Services.Contracts
{
    public interface IAttributeConditionService
    {
        Task<IEnumerable<AttributeConditionLibCm>> GetAttributeConditions();
        Task<AttributeConditionLibCm> UpdateAttributeCondition(AttributeConditionLibAm dataAm, int id);
        Task<AttributeConditionLibCm> CreateAttributeCondition(AttributeConditionLibAm dataAm);
        Task CreateAttributeConditions(List<AttributeConditionLibAm> dataAm);
    }
}