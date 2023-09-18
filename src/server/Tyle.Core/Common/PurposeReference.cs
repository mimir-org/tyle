namespace Tyle.Core.Common;

public class PurposeReference : RdlReference
{
    /// <summary>
    /// Creates a new purpose reference.
    /// </summary>
    /// <param name="name">The display name of the purpose reference.</param>
    /// <param name="iri">The reference IRI.</param>
    /// <param name="description">A description of the purpose reference. Can be null.</param>
    /// <param name="source">The source of the purpose reference. The default value is user submission.</param>
    /// /// <exception cref="ArgumentException">Thrown if the given IRI is not an absolute URI.</exception>
    public PurposeReference(string name, Uri iri, string? description,
        ReferenceSource source = ReferenceSource.UserSubmission) : base(name, iri, description, source)
    {
    }
}