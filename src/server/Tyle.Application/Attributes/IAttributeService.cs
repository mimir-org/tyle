using Tyle.Application.Attributes.Requests;
using Tyle.Application.Attributes.Views;
using Tyle.Core.Common.Exceptions;

namespace Tyle.Application.Attributes;

public interface IAttributeService
{
    /// <summary>
    /// Get all attribute types.
    /// </summary>
    /// <returns>An enumerable of all attribute types.</returns>
    Task<IEnumerable<AttributeTypeView>> GetAll();

    /// <summary>
    /// Get an attribute type by id.
    /// </summary>
    /// <param name="id">The id of the attribute type to get.</param>
    /// <returns>The attribute type with the given id.</returns>
    /// <exception cref="MimirorgNotFoundException">Thrown if there is no attribute type with the given id.</exception>
    Task<AttributeTypeView> Get(Guid id);

    /// <summary>
    /// Create a new attribute type.
    /// </summary>
    /// <param name="request">The request containing the information needed to create the attribute type.</param>
    /// <returns>The created attribute type.</returns>
    /// <exception cref="MimirorgBadRequestException">Thrown if the request is not valid.</exception>
    Task<AttributeTypeView> Create(AttributeTypeRequest request);

    /// <summary>
    /// Update an existing attribute type.
    /// </summary>
    /// <param name="id">The id of the attribute type that should be updated.</param>
    /// <param name="request">The request containing the information needed to update the attribute type.</param>
    /// <returns>The updated attribute type.</returns>
    /// <exception cref="MimirorgNotFoundException">Thrown if there is no attribute type with the given id.</exception>
    /// <exception cref="MimirorgBadRequestException">Thrown if the request is not valid.</exception>
    Task<AttributeTypeView> Update(Guid id, AttributeTypeRequest request);

    /// <summary>
    /// Delete an attribute type.
    /// </summary>
    /// <param name="id">The id of the attribute type to delete.</param>
    /// <exception cref="MimirorgNotFoundException">Thrown if there is no attribute type with the given id.</exception>
    /// <exception cref="MimirorgInvalidOperationException">Thrown if the attribute type can't be deleted.</exception>
    Task Delete(Guid id);
}