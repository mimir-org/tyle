using Tyle.Core.Common;

namespace Tyle.Application.Common;

public interface IReferenceRepository<T, TRequest>
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
    /// <param name="request">A request defining the reference that should be created.</param>
    /// <returns>The created reference object.</returns>
    Task<T> Create(TRequest request);

    /// <summary>
    /// Creates new references from an external source.
    /// </summary>
    /// <param name="requests">An enumerable containing the reference requests that should be created.</param>
    /// <param name="source">The external source that the references are collected from.</param>
    Task Create(IEnumerable<TRequest> requests, ReferenceSource source);

    /// <summary>
    /// Deletes the reference with the given id.
    /// </summary>
    /// <param name="id">The id of the reference to delete.</param>
    /// <returns>True if the reference was deleted, false if it was not found.</returns>
    Task<bool> Delete(int id);
}