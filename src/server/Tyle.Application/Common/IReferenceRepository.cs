namespace Tyle.Application.Common;

public interface IReferenceRepository<T>
{
    /// <summary>
    /// Gets all references of type T.
    /// </summary>
    /// <returns>An IEnumerable of all references of type T.</returns>
    Task<IEnumerable<T>> GetAll();

    /// <summary>
    /// Gets the reference with the given id.
    /// </summary>
    /// <param name="id">The id of the reference.</param>
    /// <returns>The reference object, or null if no reference object was found.</returns>
    Task<T?> Get(int id);

    /// <summary>
    /// Creates a new reference.
    /// </summary>
    /// <param name="reference">A request with the needed information to create the reference.</param>
    /// <returns>The created reference object.</returns>
    Task<T> Create(T reference);

    /// <summary>
    /// Deletes the reference with the given id.
    /// </summary>
    /// <param name="id">The id of the reference to delete.</param>
    /// <exception cref="KeyNotFoundException">Thrown if no reference is found with the given id.</exception>
    Task Delete(int id);
}