using Mimirorg.Common.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts;

public interface IAttributeRepository
{
    /// <summary>
    /// Get the registered company on given id
    /// </summary>
    /// <param name="id">The attribute id</param>
    /// <returns>The company id of given attribute</returns>
    Task<int> HasCompany(string id);

    /// <summary>
    /// Change the state of the attribute on all listed id's
    /// </summary>
    /// <param name="state">The state to change to</param>
    /// <param name="ids">A list of attribute id's</param>
    /// <returns>The number of attributes in given state</returns>
    Task<int> ChangeState(State state, ICollection<string> ids);

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
    /// Create all attributes in a collection
    /// </summary>
    /// <param name="attributes">A collection of attributes to create</param>
    /// <returns>A list of the created attributes</returns>
    Task<ICollection<AttributeLibDm>> Create(ICollection<AttributeLibDm> attributes);

    /// <summary>
    /// Clear all entity framework change trackers
    /// </summary>
    void ClearAllChangeTrackers();
}