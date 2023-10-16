namespace Tyle.Application.Common;

public interface ITypeRepository<T, TRequest>
{
    /// <summary>
    /// Gets all entities of a type.
    /// </summary>
    /// <returns>An IEnumerable of all entities of the type.</returns>
    Task<IEnumerable<T>> GetAll();

    /// <summary>
    /// Gets the type with the given id.
    /// </summary>
    /// <param name="id">The id of the type.</param>
    /// <returns>The type object, or null if no type was found.</returns>
    Task<T?> Get(Guid id);

    /// <summary>
    /// Creates a new type.
    /// </summary>
    /// <param name="request">A request defining the type that should be created.</param>
    /// <returns>The created type.</returns>
    Task<T> Create(TRequest request);

    /// <summary>
    /// Updates the type with the given id.
    /// </summary>
    /// <param name="id">The id of the type to update.</param>
    /// <param name="request">A request defining the new values for the type.</param>
    /// <returns>The updated type, or null if no type was found.</returns>
    Task<T?> Update(Guid id, TRequest request);

    /// <summary>
    /// Deletes the type with the given id.
    /// </summary>
    /// <param name="id">The id of the type to delete.</param>
    /// <returns>True if the type was deleted, false if it was not found.</returns>
    Task<bool> Delete(Guid id);
}