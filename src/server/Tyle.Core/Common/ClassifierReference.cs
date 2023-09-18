namespace Tyle.Core.Common;

public class ClassifierReference : RdlReference
{
    /// <summary>
    /// Creates a new classifier reference.
    /// </summary>
    /// <param name="name">The display name of the classifier reference.</param>
    /// <param name="iri">The reference IRI.</param>
    /// <param name="description">A description of the classifier reference. Can be null.</param>
    /// <param name="source">The source of the classifier reference. The default value is user submission.</param>
    /// <exception cref="ArgumentException">Thrown if the given IRI is not an absolute URI.</exception>
    public ClassifierReference(string name, Uri iri, string? description,
        ReferenceSource source = ReferenceSource.UserSubmission) : base(name, iri, description, source)
    {
    }
}