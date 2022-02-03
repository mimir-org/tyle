using System.Collections.Generic;
using System.Threading.Tasks;
using TypeLibrary.Models.Models.Application;

namespace TypeLibrary.Services.Contracts
{
    public interface IConditionService
    {
        Task<IEnumerable<ConditionAm>> GetConditions();
        Task<ConditionAm> UpdateCondition(ConditionAm dataAm);
        Task<ConditionAm> CreateCondition(ConditionAm dataAm);
        Task CreateConditions(List<ConditionAm> dataAm);
    }
}