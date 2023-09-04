using Mimirorg.TypeLibrary.Enums;

namespace Mimirorg.TypeLibrary.Models.Client;

public class PurposeReferenceCm
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Iri { get; set; }
    public ReferenceSource Source { get; set; }
    public string Kind => nameof(PurposeReferenceCm);
}