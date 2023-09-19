using Tyle.Application.Attributes.Requests;
using Tyle.Application.Common.Requests;
using Tyle.Application.Terminals.Requests;
using Tyle.Core.Attributes;
using Tyle.Core.Common;
using Tyle.Core.Terminals;

namespace Tyle.Application.Common;

public interface IReferenceService
{
    /// <summary>
    /// Get all classifier references.
    /// </summary>
    /// <returns>An IEnumerable of classifier references.</returns>
    Task<IEnumerable<ClassifierReference>> GetAllClassifiers();

    /// <summary>
    /// Get all medium references.
    /// </summary>
    /// <returns>An IEnumerable of medium references.</returns>
    //Task<IEnumerable<MediumReference>> GetAllMedia();

    /// <summary>
    /// Get all predicate references.
    /// </summary>
    /// <returns>An IEnumerable of predicate references.</returns>
    //Task<IEnumerable<PredicateReference>> GetAllPredicates();

    /// <summary>
    /// Get all purpose references.
    /// </summary>
    /// <returns>An IEnumerable of predicate references.</returns>
    //Task<IEnumerable<PurposeReference>> GetAllPurposes();

    /// <summary>
    /// Get all unit references.
    /// </summary>
    /// <returns>An IEnumerable of unit references.</returns>
    //Task<IEnumerable<UnitReference>> GetAllUnits();

    /// <summary>
    /// Get a classifier reference by id.
    /// </summary>
    /// <param name="id">The id of the classifier reference to get.</param>
    /// <returns>The classifier reference with the given id, or null if no such reference was found.</returns>
    Task<ClassifierReference?> GetClassifier(int id);

    /// <summary>
    /// Get a medium reference by id.
    /// </summary>
    /// <param name="id">The id of the medium reference to get.</param>
    /// <returns>The medium reference with the given id, or null if no such reference was found.</returns>
    //Task<MediumReference?> GetMedium(int id);

    /// <summary>
    /// Get a predicate reference by id.
    /// </summary>
    /// <param name="id">The id of the predicate reference to get.</param>
    /// <returns>The predicate reference with the given id, or null if no such reference was found.</returns>
    //Task<PredicateReference?> GetPredicate(int id);

    /// <summary>
    /// Get a purpose reference by id.
    /// </summary>
    /// <param name="id">The id of the purpose reference to get.</param>
    /// <returns>The purpose reference with the given id, or null if no such reference was found.</returns>
    //Task<PurposeReference?> GetPurpose(int id);

    /// <summary>
    /// Get a unit reference by id.
    /// </summary>
    /// <param name="id">The id of the unit reference to get.</param>
    /// <returns>The unit reference with the given id, or null if no such reference was found.</returns>
    //Task<UnitReference?> GetUnit(int id);

    /// <summary>
    /// Create a new classifier reference.
    /// </summary>
    /// <param name="request">The request with the information needed to create the reference.</param>
    /// <returns>The created classifier reference.</returns>
    Task<ClassifierReference> CreateClassifier(ClassifierReferenceRequest request);

    /// <summary>
    /// Create a new medium reference.
    /// </summary>
    /// <param name="request">The request with the information needed to create the reference.</param>
    /// <returns>The created medium reference.</returns>
    //Task<MediumReference> CreateMedium(MediumReferenceRequest request);

    /// <summary>
    /// Create a new predicate reference.
    /// </summary>
    /// <param name="request">The request with the information needed to create the reference.</param>
    /// <returns>The created predicate reference.</returns>
    //Task<PredicateReference> CreatePredicate(PredicateReferenceRequest request);

    /// <summary>
    /// Create a new purpose reference.
    /// </summary>
    /// <param name="request">The request with the information needed to create the reference.</param>
    /// <returns>The created purpose reference.</returns>
    //Task<PurposeReference> CreatePurpose(PurposeReferenceRequest request);

    /// <summary>
    /// Create a new unit reference.
    /// </summary>
    /// <param name="request">The request with the information needed to create the reference.</param>
    /// <returns>The created unit reference.</returns>
    //Task<UnitReference> CreateUnit(UnitReferenceRequest request);

    /// <summary>
    /// Delete a classifier reference.
    /// </summary>
    /// <param name="id">The id of the reference to delete.</param>
    /// <exception cref="KeyNotFoundException">Thrown if no reference with the given id is found.</exception>
    Task DeleteClassifier(int id);

    /// <summary>
    /// Delete a medium reference.
    /// </summary>
    /// <param name="id">The id of the reference to delete.</param>
    /// <exception cref="KeyNotFoundException">Thrown if no reference with the given id is found.</exception>
    //Task DeleteMedium(int id);

    /// <summary>
    /// Delete a predicate reference.
    /// </summary>
    /// <param name="id">The id of the reference to delete.</param>
    /// <exception cref="KeyNotFoundException">Thrown if no reference with the given id is found.</exception>
    //Task DeletePredicate(int id);

    /// <summary>
    /// Delete a purpose reference.
    /// </summary>
    /// <param name="id">The id of the reference to delete.</param>
    /// <exception cref="KeyNotFoundException">Thrown if no reference with the given id is found.</exception>
    //Task DeletePurpose(int id);

    /// <summary>
    /// Delete a unit reference.
    /// </summary>
    /// <param name="id">The id of the reference to delete.</param>
    /// <exception cref="KeyNotFoundException">Thrown if no reference with the given id is found.</exception>
    //Task DeleteUnit(int id);
}