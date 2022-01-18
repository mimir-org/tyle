using System.Collections.Generic;
using System.Threading.Tasks;
using TypeLibrary.Models.Application;

namespace TypeLibrary.Services.Contracts
{
    public interface IEnumService
    {
        Task<IEnumerable<ConditionAm>> GetConditions();
        Task<ConditionAm> UpdateCondition(ConditionAm conditionAm);
        Task<ConditionAm> CreateCondition(ConditionAm conditionAm);
        
    }
}
