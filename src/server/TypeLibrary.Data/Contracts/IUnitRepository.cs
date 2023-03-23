using Mimirorg.Common.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts;

public interface IUnitRepository
{
    Task<int> HasCompany(int id);

    Task<int> ChangeState(State state, ICollection<int> ids);

    /// <summary>
    /// Get all units
    /// </summary>
    /// <returns>A collection of units</returns>
    IEnumerable<UnitLibDm> Get();

    UnitLibDm Get(int id);

    Task<UnitLibDm> Create(UnitLibDm unit);

    void ClearAllChangeTrackers();
}