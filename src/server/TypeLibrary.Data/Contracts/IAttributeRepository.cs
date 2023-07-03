using Mimirorg.Common.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts;

public interface IAttributeRepository
{
    /// <summary>
    /// Change the state of the attribute with the given id
    /// </summary>
    /// <param name="state">The state to change to</param>
    /// <param name="id">The attribute id</param>
    Task ChangeState(State state, string id);

    /// <summary>
    /// Get all attributes
    /// </summary>
    /// <returns>A collection of attributes</returns>
    IEnumerable<AttributeLibDm> Get();

    /// <summary>
    /// Get a specific attribute by id
    /// </summary>
    /// <param name="id">The id of the attribute to get</param>
    /// <returns>The attribute with the given id</returns>
    AttributeLibDm Get(string id);

    /// <summary>
    /// Create an attribute
    /// </summary>
    /// <param name="attribute">The attribute to be created</param>
    /// <returns>The created attribute</returns>
    Task<AttributeLibDm> Create(AttributeLibDm attribute);

    /// <summary>
    /// Clear all entity framework change trackers
    /// </summary>
    void ClearAllChangeTrackers();
}