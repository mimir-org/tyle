using System.Collections.Generic;
using System.Threading.Tasks;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts;

public interface IUnitRepository
{
    /// <summary>
    /// Get all units
    /// </summary>
    /// <returns>A collection of units</returns>
    IEnumerable<UnitLibDm> Get();

    Task<UnitLibDm> Create(UnitLibDm unit);

    void ClearAllChangeTrackers();
}