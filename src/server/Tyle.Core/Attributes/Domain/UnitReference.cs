using Tyle.Core.Common.Domain;

namespace Tyle.Core.Attributes.Domain;

public class UnitReference : RdlReference
{
    public string? Symbol { get; set; }

    public UnitReference(string name, string iri, string? symbol, string? description,
        ReferenceSource source = ReferenceSource.UserSubmission) : base(name, iri, description, source)
    {
        Symbol = symbol;
    }
}