using Mimirorg.TypeLibrary.Enums;

namespace Mimirorg.TypeLibrary.Models.Domain;

public abstract class RdlReference
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string Iri { get; set; }
    public ReferenceSource Source { get; set; }

    protected RdlReference(string name, string iri, string? description,
        ReferenceSource source = ReferenceSource.UserSubmission)
    {
        Name = name;
        Description = description;
        Iri = iri;
        Source = source;
    }
}
