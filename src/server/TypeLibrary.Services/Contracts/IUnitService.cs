using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.Common.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;

namespace TypeLibrary.Services.Contracts;

public interface IUnitService
{
    IEnumerable<UnitLibCm> Get();
    UnitLibCm Get(int id);
    Task<UnitLibCm> Create(UnitLibAm unitAm);
    Task<ApprovalDataCm> ChangeState(int id, State state);
    Task<int> GetCompanyId(int id);
}