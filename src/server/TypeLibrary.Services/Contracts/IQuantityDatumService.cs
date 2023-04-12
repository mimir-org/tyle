using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.Common.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;

namespace TypeLibrary.Services.Contracts;

public interface IQuantityDatumService
{
    /// <summary>
    /// Get all quantity datums
    /// </summary>
    /// <returns>List of quantity datums</returns>
    IEnumerable<QuantityDatumLibCm> Get();

    /// <summary>
    /// Get quantity datum by id
    /// </summary>
    /// <param name="id">The id of the quantity datum to get</param>
    /// <returns>The quantity datum with the given id</returns>
    QuantityDatumLibCm Get(string id);

    /// <summary>
    /// Get all quantity datum range specifying
    /// </summary>
    /// <returns>List of quantity datums</returns>
    IEnumerable<QuantityDatumLibCm> GetQuantityDatumRangeSpecifying();

    /// <summary>
    /// Get all quantity datum specified scopes
    /// </summary>
    /// <returns>List of quantity datums</returns>
    IEnumerable<QuantityDatumLibCm> GetQuantityDatumSpecifiedScope();

    /// <summary>
    /// Get all quantity datum with specified provenances
    /// </summary>
    /// <returns>List of quantity datums</returns>
    IEnumerable<QuantityDatumLibCm> GetQuantityDatumSpecifiedProvenance();

    /// <summary>
    /// Get all quantity datum regularity specified
    /// </summary>
    /// <returns>List of quantity datums</returns>
    IEnumerable<QuantityDatumLibCm> GetQuantityDatumRegularitySpecified();

    /// <summary>
    /// Create a new quantity datum
    /// </summary>
    /// <param name="quantityDatumAm">The quantity datum that should be created</param>
    /// <returns>The created quantity datum</returns>
    Task<QuantityDatumLibCm> Create(QuantityDatumLibAm quantityDatumAm);

    /// <summary>
    /// Change quantity datum state
    /// </summary>
    /// <param name="id">The quantity datum id that should change state</param>
    /// <param name="state">The new quantity datum state</param>
    /// <returns>Quantity datum with updated state</returns>
    Task<ApprovalDataCm> ChangeState(string id, State state);

    /// <summary>
    /// Get the company id of a quantity datum
    /// </summary>
    /// <param name="id">The quantity datum id</param>
    /// <returns>Company id for the quantity datum</returns>
    int GetCompanyId(string id);
}