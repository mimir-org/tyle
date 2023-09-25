namespace Tyle.Application.Common;

public interface ITypeRepository<T>
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
    /// <param name="type">The type that should be created.</param>
    /// <returns>The created type.</returns>
    Task<T> Create(T type);

    /// <summary>
    /// Updates the type given by the id of the input type.
    /// </summary>
    /// <param name="type">A type object containing the new values for the type.</param>
    /// <returns>The updated type.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if no type is found with the given id.</exception>
    Task<T> Update(T type);

    /// <summary>
    /// Deletes the type with the given id.
    /// </summary>
    /// <param name="id">The id of the type to delete.</param>
    /// <exception cref="KeyNotFoundException">Thrown if no type is found with the given id.</exception>
    Task Delete(Guid id);
}