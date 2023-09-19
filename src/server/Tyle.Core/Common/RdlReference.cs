namespace Tyle.Core.Common;

public abstract record RdlReference
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public Uri Iri { get; set; }
    public ReferenceSource Source { get; set; }

    protected RdlReference(string name, Uri iri, string? description,
        ReferenceSource source = ReferenceSource.UserSubmission)
    {
        if (!iri.IsAbsoluteUri)
        {
            throw new ArgumentException($"{iri} is not a valid IRI.", nameof(iri));
        }

        Name = name;
        Description = description;
        Iri = iri;
        Source = source;
    }
}
