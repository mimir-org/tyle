using Mimirorg.TypeLibrary.Enums;

namespace Mimirorg.TypeLibrary.Models.Domain;

public class UnitReference : GenericReference
{
    public string? Symbol { get; set; }

    public UnitReference(string name, string iri, string? symbol, string? description,
        ReferenceSource source = ReferenceSource.UserSubmission) : base(name, iri, description, source)
    {
        Symbol = symbol;
    }
}