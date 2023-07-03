using Mimirorg.Common.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts;

public interface IUnitRepository
{
    /// <summary>
    /// Change the state of the unit with the given id
    /// </summary>
    /// <param name="state">The state to change to</param>
    /// <param name="id">The unit id</param>
    Task ChangeState(State state, string id);

    /// <summary>
    /// Get all units
    /// </summary>
    /// <returns>A collection of units</returns>
    IEnumerable<UnitLibDm> Get();

    /// <summary>
    /// Get a specific unit by id
    /// </summary>
    /// <param name="id">The id of the unit to get</param>
    /// <returns>The unit with the given id</returns>
    UnitLibDm Get(string id);

    /// <summary>
    /// Get a specific unit by type reference
    /// </summary>
    /// <param name="typeReference">The type reference of the unit to get</param>
    /// <returns>The unit with the given type reference</returns>
    UnitLibDm GetByTypeReference(string typeReference);

    /// <summary>
    /// Create a unit
    /// </summary>
    /// <param name="unit">The unit to be created</param>
    /// <returns>The created unit</returns>
    Task<UnitLibDm> Create(UnitLibDm unit);

    /// <summary>
    /// Clear all entity framework change trackers
    /// </summary>
    void ClearAllChangeTrackers();
}