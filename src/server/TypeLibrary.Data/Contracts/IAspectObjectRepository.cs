using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.Common.Enums;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts;

public interface IAspectObjectRepository
{
    /// <summary>
    /// Get the registered company on given id
    /// </summary>
    /// <param name="id">The aspect object id</param>
    /// <returns>The company id of given aspect object</returns>
    int HasCompany(string id);

    /// <summary>
    /// Change the state of the aspect object with the given id
    /// </summary>
    /// <param name="state">The state to change to</param>
    /// <param name="id">The aspect object id</param>
    Task ChangeState(State state, string id);

    /// <summary>
    /// Get all aspect objects
    /// </summary>
    /// <returns>A collection of aspect objects</returns>
    IEnumerable<AspectObjectLibDm> Get();

    /// <summary>
    /// Get aspect object by id
    /// </summary>
    /// <param name="id">The aspect object id</param>
    /// <returns>Aspect object if found</returns>
    AspectObjectLibDm Get(string id);

    /// <summary>
    /// Get all versions of the given aspect object
    /// </summary>
    /// <param name="aspectObject">The aspect object</param>
    /// <returns>A collection of all versions of the aspect object</returns>
    IEnumerable<AspectObjectLibDm> GetAllVersions(AspectObjectLibDm aspectObject);

    /// <summary>
    /// Create a aspect object
    /// </summary>
    /// <param name="aspectObject">The aspect object to be created</param>
    /// <returns>The created aspect object</returns>
    Task<AspectObjectLibDm> Create(AspectObjectLibDm aspectObject);

    /// <summary>
    /// Clear all entity framework change trackers
    /// </summary>
    void ClearAllChangeTrackers();
}