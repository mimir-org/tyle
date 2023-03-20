using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.Common.Enums;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts;

public interface INodeRepository
{
    /// <summary>
    /// Get the registered company on given id
    /// </summary>
    /// <param name="id">The node id</param>
    /// <returns>The company id of given node</returns>
    Task<int> HasCompany(int id);

    /// <summary>
    /// Change the state of the node on all listed id's
    /// </summary>
    /// <param name="state">The state to change to</param>
    /// <param name="ids">A list of node id's</param>
    /// <returns>The number of nodes in given state</returns>
    Task<int> ChangeState(State state, ICollection<int> ids);

    /// <summary>
    /// Change all parent id's on nodes from old id to the new id 
    /// </summary>
    /// <param name="oldId">Old node parent id</param>
    /// <param name="newId">New node parent id</param>
    /// <returns>The number of nodes with the new parent id</returns>
    Task<int> ChangeParentId(int oldId, int newId);

    /// <summary>
    /// Check if node exists
    /// </summary>
    /// <param name="id">The id of the node</param>
    /// <returns>True if node exist</returns>
    Task<bool> Exist(int id);

    /// <summary>
    /// Get all nodes
    /// </summary>
    /// <returns>A collection of nodes</returns>
    IEnumerable<NodeLibDm> Get();

    /// <summary>
    /// Get node by id
    /// </summary>
    /// <param name="id">The node id</param>
    /// <returns>Node if found</returns>
    Task<NodeLibDm> Get(int id);

    /// <summary>
    /// Create a node
    /// </summary>
    /// <param name="node">The node to be created</param>
    /// <returns>The created node</returns>
    Task<NodeLibDm> Create(NodeLibDm node);

    /// <summary>
    /// Clear all entity framework change trackers
    /// </summary>
    void ClearAllChangeTrackers();
}