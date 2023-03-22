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
    UnitLibCm Get(int id);

    /// <summary>
    /// Create a new unit
    /// </summary>
    /// <param name="unitAm">The unit that should be created</param>
    /// <returns>The created unit</returns>
    Task<UnitLibCm> Create(UnitLibAm unitAm);

    /// <summary>
    /// Change unit state
    /// </summary>
    /// <param name="id">The unit id that should change state</param>
    /// <param name="state">The new unit state</param>
    /// <returns>Unit with updated state</returns>
    Task<ApprovalDataCm> ChangeState(int id, State state);

    /// <summary>
    /// Get the company id of a unit
    /// </summary>
    /// <param name="id">The unit id</param>
    /// <returns>Company id for the unit</returns>
    Task<int> GetCompanyId(int id);
}