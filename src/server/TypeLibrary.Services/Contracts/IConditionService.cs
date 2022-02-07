using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Models.Application;

namespace TypeLibrary.Services.Contracts
{
    public interface IConditionService
    {
        Task<IEnumerable<ConditionLibAm>> GetConditions();
        Task<ConditionLibAm> UpdateCondition(ConditionLibAm dataAm);
        Task<ConditionLibAm> CreateCondition(ConditionLibAm dataAm);
        Task CreateConditions(List<ConditionLibAm> dataAm);
    }
}