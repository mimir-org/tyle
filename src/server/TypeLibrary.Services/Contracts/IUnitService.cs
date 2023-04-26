using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.Common.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;

namespace TypeLibrary.Services.Contracts;

public interface IUnitService
{
    /// <summary>
    /// Get all units
    /// </summary>
    /// <returns>List of units</returns>
    IEnumerable<UnitLibCm> Get();

    /// <summary>
    /// Get a unit by id
    /// </summary>
    /// <param name="id">The id of the unit</param>
    /// <returns>The unit with the given id</returns>
    UnitLibCm Get(string id);

    /// <summary>
    /// Create a new unit
    /// </summary>
    /// <param name="unitAm">The unit that should be created</param>
    /// <param name="createdBy">Used to set created by value for instances where objects are not created by the user</param>
    /// <returns>The created unit</returns>
    Task<UnitLibCm> Create(UnitLibAm unitAm, string createdBy = null);

    /// <summary>
    /// Update an existing unit
    /// </summary>
    /// <param name="id">The id of the unit that should be updated</param>
    /// <param name="unitAm">The new unit values</param>
    /// <returns>The updated unit</returns>
    Task<UnitLibCm> Update(string id, UnitLibAm unitAm);

    /// <summary>
    /// Change unit state
    /// </summary>
    /// <param name="id">The unit id that should change state</param>
    /// <param name="state">The new unit state</param>
    /// <returns>Unit with updated state</returns>
    Task<ApprovalDataCm> ChangeState(string id, State state);
}