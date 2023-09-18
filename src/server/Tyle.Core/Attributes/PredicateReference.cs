using Tyle.Core.Common;

namespace Tyle.Core.Attributes;

public record PredicateReference : RdlReference
{
    /// <summary>
    /// Creates a new predicate reference.
    /// </summary>
    /// <param name="name">The display name of the predicate reference.</param>
    /// <param name="iri">The reference IRI.</param>
    /// <param name="description">A description of the predicate reference. Can be null.</param>
    /// <param name="source">The source of the predicate reference. The default value is user submission.</param>
    /// <exception cref="ArgumentException">Thrown if the given IRI is not an absolute URI.</exception>
    public PredicateReference(string name, Uri iri, string? description,
        ReferenceSource source = ReferenceSource.UserSubmission) : base(name, iri, description, source)
    {
    }
}