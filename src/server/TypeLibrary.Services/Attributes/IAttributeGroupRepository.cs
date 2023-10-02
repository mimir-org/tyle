using TypeLibrary.Core.Attributes;
using TypeLibrary.Services.Attributes.Requests;

namespace TypeLibrary.Services.Attributes;

public interface IAttributeGroupRepository
{
    /// <summary>
    /// Gets all attribute groups.
    /// </summary>
    /// <returns>An IEnumerable of all attribute groups.</returns>
    Task<IEnumerable<AttributeGroup>> GetAll();

    /// <summary>
    /// Gets the attribute group with the given id.
    /// </summary>
    /// <param name="id">The id of the attribute group.</param>
    /// <returns>The attribute group, or null if no attribute group was found.</returns>
    Task<AttributeGroup?> Get(Guid id);

    /// <summary>
    /// Creates a new attribute group.
    /// </summary>
    /// <param name="request">A request defining the attribute group that should be created.</param>
    /// <returns>The created attribute group.</returns>
    Task<AttributeGroup> Create(AttributeGroupRequest request);

    /// <summary>
    /// Updates the attribute group with the given id.
    /// </summary>
    /// <param name="id">The id of the attribute group to update.</param>
    /// <param name="request">A request defining the new values for the attribute group.</param>
    /// <returns>The updated attribute group, or null if no attribute group was found.</returns>
    Task<AttributeGroup?> Update(Guid id, AttributeGroupRequest request);

    /// <summary>
    /// Deletes the attribute group with the given id.
    /// </summary>
    /// <param name="id">The id of the attribute group to delete.</param>
    /// <returns>True if the attribute group was deleted, false if it was not found.</returns>
    Task<bool> Delete(Guid id);
}