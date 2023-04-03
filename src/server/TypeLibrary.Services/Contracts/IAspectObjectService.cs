using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.Common.Enums;
using Mimirorg.Common.Exceptions;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;

namespace TypeLibrary.Services.Contracts;

public interface IAspectObjectService
{
    /// <summary>
    /// Get the latest aspect object versions
    /// </summary>
    /// <returns>A collection of aspect objects</returns>
    IEnumerable<AspectObjectLibCm> GetLatestVersions();

    /// <summary>
    /// Get the latest version of an aspect object based on given id
    /// </summary>
    /// <param name="id">The id of the aspect object</param>
    /// <returns>The latest version of the aspect object of given id</returns>
    /// <exception cref="MimirorgNotFoundException">Throws if there is no aspect object with the given id, and that aspect object is at the latest version.</exception>
    AspectObjectLibCm Get(int id);

    /// <summary>
    /// Create a new aspect object
    /// </summary>
    /// <param name="aspectObjectAm">The aspect object that should be created</param>
    /// <returns>The created aspect object</returns>
    /// <exception cref="MimirorgBadRequestException">Throws if aspect object is not valid</exception>
    /// <exception cref="MimirorgDuplicateException">Throws if aspect object already exist</exception>
    /// <remarks>Remember that creating a new aspect object could be creating a new version of existing aspect object.
    /// They will have the same first version id, but have different version and id.</remarks>
    Task<AspectObjectLibCm> Create(AspectObjectLibAm aspectObjectAm);

    /// <summary>
    /// Update an aspect object if the data is allowed to be changed.
    /// </summary>
    /// <param name="id">The id of the aspect object to update</param>
    /// <param name="aspectObjectAm">The aspect object to update</param>
    /// <returns>The updated aspect object</returns>
    /// <exception cref="MimirorgBadRequestException">Throws if the aspect object does not exist,
    /// if it is not valid or there are not allowed changes.</exception>
    /// <remarks>ParentId to old references will also be updated.</remarks>
    Task<AspectObjectLibCm> Update(int id, AspectObjectLibAm aspectObjectAm);

    /// <summary>
    /// Change aspect object state
    /// </summary>
    /// <param name="id">The aspect object id that should change the state</param>
    /// <param name="state">The new aspect object state</param>
    /// <returns>Aspect object with updated state</returns>
    /// <exception cref="MimirorgNotFoundException">Throws if the aspect object does not exist on latest version</exception>
    Task<ApprovalDataCm> ChangeState(int id, State state);

    /// <summary>
    /// Get the company id of an aspect object
    /// </summary>
    /// <param name="id">The aspect object id</param>
    /// <returns>Company id for the aspect object</returns>
    Task<int> GetCompanyId(int id);
}