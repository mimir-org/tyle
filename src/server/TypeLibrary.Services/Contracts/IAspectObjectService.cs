using Mimirorg.Common.Enums;
using Mimirorg.Common.Exceptions;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TypeLibrary.Services.Contracts;

public interface IAspectObjectService
{
    /// <summary>
    /// Get the latest approved aspect object versions as well as any drafts
    /// </summary>
    /// <returns>A collection of aspect objects</returns>
    IEnumerable<AspectObjectLibCm> GetLatestApprovedAndDrafts();

    /// <summary>
    /// Get the latest aspect object versions with request states (approve and delete)
    /// </summary>
    /// <returns>A collection of aspect objects</returns>
    IEnumerable<AspectObjectLibCm> GetLatestRequests();

    /// <summary>
    /// Get an aspect object based on given id
    /// </summary>
    /// <param name="id">The id of the aspect object</param>
    /// <returns>The aspect object of given id</returns>
    /// <exception cref="MimirorgNotFoundException">Throws if there is no aspect object with the given id.</exception>
    AspectObjectLibCm Get(string id);

    /// <summary>
    /// Get the latest approved version of an aspect object
    /// </summary>
    /// <param name="id">The id of the aspect object we want to get the latest approved version of</param>
    /// <returns>The latest approved version of the aspect object of given id</returns>
    /// <exception cref="MimirorgNotFoundException">Throws if there is no latest approved aspect object with the given id.</exception>
    AspectObjectLibCm GetLatestApproved(string id);

    /// <summary>
    /// Create a new aspect object
    /// </summary>
    /// <param name="aspectObjectAm">The aspect object that should be created</param>
    /// <returns>The created aspect object</returns>
    /// <exception cref="MimirorgBadRequestException">Throws if aspect object is not valid</exception>
    /// <remarks>Remember that creating a new aspect object could be creating a new version of existing aspect object.
    /// They will have the same first version id, but have different version and id.</remarks>
    Task<AspectObjectLibCm> Create(AspectObjectLibAm aspectObjectAm);

    /// <summary>
    /// Update an aspect object if the data is allowed to be changed.
    /// </summary>
    /// <param name="id">The id of the aspect object to update</param>
    /// <param name="aspectObjectAm">The new aspect object values</param>
    /// <returns>The updated aspect object</returns>
    /// <exception cref="MimirorgNotFoundException">Throws if the aspect object with the given id is not found.</exception>
    /// <exception cref="MimirorgBadRequestException">Throws if the aspect object is not valid.</exception>
    /// <exception cref="MimirorgInvalidOperationException">Throws if the aspect object is a state that makes it invalid for updates,
    /// a draft already exists for this type or if changes are not allowed.</exception>
    Task<AspectObjectLibCm> Update(string id, AspectObjectLibAm aspectObjectAm);

    /// <summary>
    /// Change aspect object state
    /// </summary>
    /// <param name="id">The id of the aspect object that should change state</param>
    /// <param name="state">The new aspect object state</param>
    /// <param name="sendStateEmail"></param>
    /// <returns>An approval data object</returns>
    /// <exception cref="MimirorgNotFoundException">Throws if the aspect object does not exist</exception>
    /// <exception cref="MimirorgInvalidOperationException">Throws if the aspect object is already
    /// approved, is identical to an already approved aspect object or contains references to deleted or unapproved
    /// terminals, attributes or RDS.</exception>
    Task<ApprovalDataCm> ChangeState(string id, State state, bool sendStateEmail = true);

    /// <summary>
    /// Get the company id of an aspect object
    /// </summary>
    /// <param name="id">The aspect object id</param>
    /// <returns>Company id for the aspect object</returns>
    int GetCompanyId(string id);
}