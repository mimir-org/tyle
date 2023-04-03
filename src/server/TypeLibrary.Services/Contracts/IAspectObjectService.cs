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
    /// Get the latest node versions
    /// </summary>
    /// <returns>A collection of nodes</returns>
    IEnumerable<AspectObjectLibCm> GetLatestVersions();

    /// <summary>
    /// Get the latest version of a node based on given id
    /// </summary>
    /// <param name="id">The id of the node</param>
    /// <returns>The latest version of the node of given id</returns>
    /// <exception cref="MimirorgNotFoundException">Throws if there is no node with the given id, and that node is at the latest version.</exception>
    AspectObjectLibCm Get(int id);

    /// <summary>
    /// Create a new node
    /// </summary>
    /// <param name="aspectObjectAm">The node that should be created</param>
    /// <returns>The created node</returns>
    /// <exception cref="MimirorgBadRequestException">Throws if node is not valid</exception>
    /// <exception cref="MimirorgDuplicateException">Throws if node already exist</exception>
    /// <remarks>Remember that creating a new node could be creating a new version of existing node.
    /// They will have the same first version id, but have different version and id.</remarks>
    Task<AspectObjectLibCm> Create(AspectObjectLibAm aspectObjectAm);

    /// <summary>
    /// Update a node if the data is allowed to be changed.
    /// </summary>
    /// <param name="id">The id of the node to update</param>
    /// <param name="aspectObjectAm">The node to update</param>
    /// <returns>The updated node</returns>
    /// <exception cref="MimirorgBadRequestException">Throws if the node does not exist,
    /// if it is not valid or there are not allowed changes.</exception>
    /// <remarks>ParentId to old references will also be updated.</remarks>
    Task<AspectObjectLibCm> Update(int id, AspectObjectLibAm aspectObjectAm);

    /// <summary>
    /// Change node state
    /// </summary>
    /// <param name="id">The node id that should change the state</param>
    /// <param name="state">The new node state</param>
    /// <returns>Node with updated state</returns>
    /// <exception cref="MimirorgNotFoundException">Throws if the node does not exist on latest version</exception>
    Task<ApprovalDataCm> ChangeState(int id, State state);

    /// <summary>
    /// Get the company id of a node
    /// </summary>
    /// <param name="id">The node id</param>
    /// <returns>Company id for the node</returns>
    Task<int> GetCompanyId(int id);
}