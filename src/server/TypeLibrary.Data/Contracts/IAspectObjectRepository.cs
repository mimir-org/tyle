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
    /// Change the state of the aspect object on all listed ids
    /// </summary>
    /// <param name="state">The state to change to</param>
    /// <param name="ids">A list of aspect object ids</param>
    /// <returns>The number of aspect objects with changed state</returns>
    Task<int> ChangeState(State state, ICollection<string> ids);

    /// <summary>
    /// Change all parent ids on aspect objects from old id to the new id 
    /// </summary>
    /// <param name="oldId">Old aspect object parent id</param>
    /// <param name="newId">New aspect object parent id</param>
    /// <returns>The number of aspect objects with the new parent id</returns>
    Task<int> ChangeParentId(string oldId, string newId);

    /// <summary>
    /// Check if aspect object exists
    /// </summary>
    /// <param name="id">The id of the aspect object</param>
    /// <returns>True if aspect object exist</returns>
    Task<bool> Exist(string id);

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