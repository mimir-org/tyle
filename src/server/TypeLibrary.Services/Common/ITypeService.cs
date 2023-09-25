namespace Tyle.Application.Common;

public interface ITypeService<T, TRequest>
{
    /// <summary>
    /// Get all types of type T.
    /// </summary>
    /// <returns>An enumerable of all types.</returns>
    Task<IEnumerable<T>> GetAll();

    /// <summary>
    /// Get a type by id.
    /// </summary>
    /// <param name="id">The id of the type to get.</param>
    /// <returns>The type with the given id, or null if no such type is found.</returns>
    Task<T?> Get(Guid id);

    /// <summary>
    /// Create a new type.
    /// </summary>
    /// <param name="request">The request containing the information needed to create the type.</param>
    /// <returns>The created type.</returns>
    /// <exception cref="ArgumentException">Thrown if the request information hinders creation,
    /// for instance if a reference id is invalid.</exception>
    Task<T> Create(TRequest request);

    /// <summary>
    /// Update an existing type.
    /// </summary>
    /// <param name="id">The id of the type that should be updated.</param>
    /// <param name="request">The request containing the information needed to update the type.</param>
    /// <returns>The updated type.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if there is no type with the given id.</exception>
    /// <exception cref="ArgumentException">Thrown if the request information hinders the update of the type,
    /// for instance if a reference id is invalid.</exception>
    Task<T> Update(Guid id, TRequest request);

    /// <summary>
    /// Delete a type.
    /// </summary>
    /// <param name="id">The id of the type to delete.</param>
    /// <exception cref="KeyNotFoundException">Thrown if there is no type with the given id.</exception>
    Task Delete(Guid id);
}