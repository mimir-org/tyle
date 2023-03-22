using Mimirorg.Common.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts;

public interface IAttributeRepository
{
    Task<int> HasCompany(int id);

    Task<int> ChangeState(State state, ICollection<int> ids);

    /// <summary>
    /// Get all attributes
    /// </summary>
    /// <returns>A collection of attributes</returns>
    IEnumerable<AttributeLibDm> Get();

    /// <summary>
    /// Create a node
    /// </summary>
    /// <param name="node">The node to be created</param>
    /// <returns>The created node</returns>
    Task<AttributeLibDm> Create(AttributeLibDm node);

    /// <summary>
    /// Clear all entity framework change trackers
    /// </summary>
    void ClearAllChangeTrackers();
}