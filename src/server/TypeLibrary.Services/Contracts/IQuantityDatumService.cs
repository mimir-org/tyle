using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.Common.Enums;
using Mimirorg.Common.Exceptions;
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
    /// <exception cref="MimirorgNotFoundException">Throws if there is no quantity datum with the given id.</exception>
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
    /// <param name="createdBy">Used to set created by value for instances where objects are not created by the user</param>
    /// <returns>The created quantity datum</returns>
    Task<QuantityDatumLibCm> Create(QuantityDatumLibAm quantityDatumAm, string createdBy = null);

    /// <summary>
    /// Update an existing quantity datum
    /// </summary>
    /// <param name="id">The id of the quantity datum that should be updated</param>
    /// <param name="quantityDatumAm">The new quantity datum values</param>
    /// <returns>The updated quantity datum</returns>
    /// <exception cref="MimirorgNotFoundException">Throws if there is no quantity datum with the given id.</exception>
    /// <exception cref="MimirorgBadRequestException">Throws if the quantity datum is not valid.</exception>
    /// <exception cref="MimirorgInvalidOperationException">Throws if the quantity datum is not a draft or approved.</exception>
    Task<QuantityDatumLibCm> Update(string id, QuantityDatumLibAm quantityDatumAm);

    /// <summary>
    /// Change quantity datum state
    /// </summary>
    /// <param name="id">The id of the quantity datum that should change state</param>
    /// <param name="state">The new quantity datum state</param>
    /// <returns>An approval data object</returns>
    /// <exception cref="MimirorgNotFoundException">Throws if the quantity datum does not exist</exception>
    /// <exception cref="MimirorgInvalidOperationException">Throws if the quantity datum is already approved.</exception>
    Task<ApprovalDataCm> ChangeState(string id, State state);
}