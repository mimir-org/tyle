namespace Tyle.Core.Common;

public abstract class RdlReference
{
    public int Id { get; }
    public string Name { get; }
    public string? Description { get; }
    public string Iri { get; }
    public ReferenceSource Source { get; }

    protected RdlReference(string name, string iri, string? description,
        ReferenceSource source = ReferenceSource.UserSubmission)
    {
        Name = name;
        Description = description;
        Iri = iri;
        Source = source;
    }
}
