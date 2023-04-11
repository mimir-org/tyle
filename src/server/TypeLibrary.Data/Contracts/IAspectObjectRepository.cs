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
    Task<int> HasCompany(int id);

    /// <summary>
    /// Change the state of the aspect object on all listed id's
    /// </summary>
    /// <param name="state">The state to change to</param>
    /// <param name="ids">A list of aspect object id's</param>
    /// <returns>The number of aspect objects in given state</returns>
    Task<int> ChangeState(State state, ICollection<int> ids);

    /// <summary>
    /// Change all parent id's on aspect objects from old id to the new id 
    /// </summary>
    /// <param name="oldId">Old aspect object parent id</param>
    /// <param name="newId">New aspect object parent id</param>
    /// <returns>The number of aspect objects with the new parent id</returns>
    Task<int> ChangeParentId(int oldId, int newId);

    /// <summary>
    /// Check if aspect object exists
    /// </summary>
    /// <param name="id">The id of the aspect object</param>
    /// <returns>True if aspect object exist</returns>
    Task<bool> Exist(int id);

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
    AspectObjectLibDm Get(int id);

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