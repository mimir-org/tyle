using Tyle.Application.Attributes.Requests;
using Tyle.Core.Attributes;

namespace Tyle.Application.Attributes;

public interface IAttributeService
{
    /// <summary>
    /// Get all attribute types.
    /// </summary>
    /// <returns>An enumerable of all attribute types.</returns>
    Task<IEnumerable<AttributeType>> GetAll();

    /// <summary>
    /// Get an attribute type by id.
    /// </summary>
    /// <param name="id">The id of the attribute type to get.</param>
    /// <returns>The attribute type with the given id, or null if no such attribute type is found.</returns>
    Task<AttributeType?> Get(Guid id);

    /// <summary>
    /// Create a new attribute type.
    /// </summary>
    /// <param name="request">The request containing the information needed to create the attribute type.</param>
    /// <returns>The created attribute type.</returns>
    /// <exception cref="ArgumentException">Thrown if the request information hinders attribute creation,
    /// for instance if a reference id is invalid.</exception>
    Task<AttributeType> Create(AttributeTypeRequest request);

    /// <summary>
    /// Update an existing attribute type.
    /// </summary>
    /// <param name="id">The id of the attribute type that should be updated.</param>
    /// <param name="request">The request containing the information needed to update the attribute type.</param>
    /// <returns>The updated attribute type.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if there is no attribute type with the given id.</exception>
    /// <exception cref="ArgumentException">Thrown if the request information hinders the update of the attribute,
    /// for instance if a reference id is invalid.</exception>
    Task<AttributeType> Update(Guid id, AttributeTypeRequest request);

    /// <summary>
    /// Delete an attribute type.
    /// </summary>
    /// <param name="id">The id of the attribute type to delete.</param>
    /// <exception cref="KeyNotFoundException">Thrown if there is no attribute type with the given id.</exception>
    Task Delete(Guid id);
}