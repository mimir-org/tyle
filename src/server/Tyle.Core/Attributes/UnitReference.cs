using Tyle.Core.Common;

namespace Tyle.Core.Attributes;

public record UnitReference : RdlReference
{
    public string? Symbol { get; set; }

    /// <summary>
    /// Creates a new unit reference.
    /// </summary>
    /// <param name="name">The display name of the unit reference.</param>
    /// <param name="iri">The reference IRI.</param>
    /// <param name="symbol">The symbol of the referenced unit. Can be null.</param>
    /// <param name="description">A description of the unit reference. Can be null.</param>
    /// <param name="source">The source of the unit reference. The default value is user submission.</param>
    /// <exception cref="ArgumentException">Thrown if the given IRI is not an absolute URI.</exception>
    public UnitReference(string name, Uri iri, string? symbol, string? description,
        ReferenceSource source = ReferenceSource.UserSubmission) : base(name, iri, description, source)
    {
        Symbol = symbol;
    }
}