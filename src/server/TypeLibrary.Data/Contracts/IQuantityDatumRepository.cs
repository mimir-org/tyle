using Mimirorg.Common.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts;

public interface IQuantityDatumRepository
{
    /// <summary>
    /// Get all quantity datums
    /// </summary>
    /// <returns>A collection of quantity datums</returns>
    IEnumerable<QuantityDatumLibDm> Get();

    /// <summary>
    /// Get a specific quantity datum by id
    /// </summary>
    /// <param name="id">The id of the quantity datum to get</param>
    /// <returns>The quantity datum with the given id</returns>
    QuantityDatumLibDm Get(string id);

    /// <summary>
    /// Get all quantity datum range specifying
    /// </summary>
    /// <returns>A collection of quantity datums</returns>
    IEnumerable<QuantityDatumLibDm> GetQuantityDatumRangeSpecifying();

    /// <summary>
    /// Get all quantity datum specified scopes
    /// </summary>
    /// <returns>A collection of quantity datums</returns>
    IEnumerable<QuantityDatumLibDm> GetQuantityDatumSpecifiedScope();

    /// <summary>
    /// Get all quantity datum with specified provenances
    /// </summary>
    /// <returns>A collection of quantity datums</returns>
    IEnumerable<QuantityDatumLibDm> GetQuantityDatumSpecifiedProvenance();

    /// <summary>
    /// Get all quantity datum regularity specified
    /// </summary>
    /// <returns>A collection of quantity datums</returns>
    IEnumerable<QuantityDatumLibDm> GetQuantityDatumRegularitySpecified();

    /// <summary>
    /// Create a quantity datum
    /// </summary>
    /// <param name="quantityDatum">The quantity datum to be created</param>
    /// <returns>The created quantity datum</returns>
    Task<QuantityDatumLibDm> Create(QuantityDatumLibDm quantityDatum);

    /// <summary>
    /// Create all quantity datums in a collection
    /// </summary>
    /// <param name="quantityDatums">A collection of quantity datums to create</param>
    /// <returns>A list of the created quantity datums</returns>
    Task<ICollection<QuantityDatumLibDm>> Create(ICollection<QuantityDatumLibDm> quantityDatums);

    /// <summary>
    /// Change the state of the quantity datum with the given id
    /// </summary>
    /// <param name="state">The state to change to</param>
    /// <param name="id">The quantity datum id</param>
    Task ChangeState(State state, string id);

    /// <summary>
    /// Change the state of the quantity datums on all listed ids
    /// </summary>
    /// <param name="state">The state to change to</param>
    /// <param name="ids">A list of quantity datum ids</param>
    /// <returns>The number of quantity datums with changed state</returns>
    Task<int> ChangeState(State state, ICollection<string> ids);

    /// <summary>
    /// Clear all entity framework change trackers
    /// </summary>
    void ClearAllChangeTrackers();
}