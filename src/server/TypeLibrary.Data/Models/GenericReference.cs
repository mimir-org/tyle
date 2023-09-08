using Mimirorg.TypeLibrary.Enums;

namespace TypeLibrary.Data.Models;

public abstract class GenericReference
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string Iri { get; set; }
    public ReferenceSource Source { get; set; }

    protected GenericReference(string name, string iri, string? description,
        ReferenceSource source = ReferenceSource.UserSubmission)
    {
        Name = name;
        Description = description;
        Iri = iri;
        Source = source;
    }
}
