using Mimirorg.TypeLibrary.Enums;

namespace Mimirorg.TypeLibrary.Models.Client;

public class UnitReferenceCm
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Symbol { get; set; }
    public string Description { get; set; }
    public string Iri { get; set; }
    public ReferenceSource Source { get; set; }
    public string Kind => nameof(UnitReferenceCm);
}