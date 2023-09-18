using Tyle.Core.Common;

namespace Tyle.Core.Terminals;

public class MediumReference : RdlReference
{
    /// <summary>
    /// Creates a new medium reference.
    /// </summary>
    /// <param name="name">The display name of the medium reference.</param>
    /// <param name="iri">The reference IRI.</param>
    /// <param name="description">A description of the medium reference. Can be null.</param>
    /// <param name="source">The source of the medium reference. The default value is user submission.</param>
    /// <exception cref="ArgumentException">Thrown if the given IRI is not an absolute URI.</exception>
    public MediumReference(string name, Uri iri, string? description,
        ReferenceSource source = ReferenceSource.UserSubmission) : base(name, iri, description, source)
    {
    }
}