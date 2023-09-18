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
    /// <param name="request">A request with the needed information to create the type.</param>
    /// <returns>The created type.</returns>
    Task<T> Create(TRequest request);

    /// <summary>
    /// Updates the type with the given id.
    /// </summary>
    /// <param name="id">The id of the type to update.</param>
    /// <param name="request">A request with the needed information to update the type.</param>
    /// <returns>The updated type.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if no type is found with the given id.</exception>
    Task<T> Update(Guid id, TRequest request);

    /// <summary>
    /// Deletes the type with the given id.
    /// </summary>
    /// <param name="id">The id of the type to delete.</param>
    /// <exception cref="KeyNotFoundException">Thrown if no type is found with the given id.</exception>
    Task Delete(int id);
}