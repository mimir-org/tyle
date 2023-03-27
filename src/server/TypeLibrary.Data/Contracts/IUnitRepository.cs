using Mimirorg.Common.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts;

public interface IUnitRepository
{
    /// <summary>
    /// Get the registered company on given id
    /// </summary>
    /// <param name="id">The unit id</param>
    /// <returns>The company id of given unit</returns>
    Task<int> HasCompany(int id);

    /// <summary>
    /// Change the state of the unit on all listed id's
    /// </summary>
    /// <param name="state">The state to change to</param>
    /// <param name="ids">A list of unit id's</param>
    /// <returns>The number of units in given state</returns>
    Task<int> ChangeState(State state, ICollection<int> ids);

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
    UnitLibDm Get(int id);

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
    /// Create all units in a collection
    /// </summary>
    /// <param name="units">A collection of units to create</param>
    /// <returns>A list of the created units</returns>
    Task<ICollection<UnitLibDm>> Create(ICollection<UnitLibDm> units);

    /// <summary>
    /// Clear all entity framework change trackers
    /// </summary>
    void ClearAllChangeTrackers();
}