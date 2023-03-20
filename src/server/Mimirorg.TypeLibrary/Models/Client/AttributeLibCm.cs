namespace Mimirorg.TypeLibrary.Models.Client;

public class AttributeLibCm
{
    public string Name { get; set; }
    public string Iri { get; set; }
    public string Source { get; set; }
    public ICollection<UnitLibCm> Units { get; set; }

    public string Id => Iri?[(Iri.LastIndexOf('/') + 1)..];
    public string Kind => nameof(AttributeLibCm);
}