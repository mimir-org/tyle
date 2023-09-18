namespace Tyle.Core.Common;

public abstract record RdlReference
{
    public int Id { get; }
    public string Name { get; }
    public string? Description { get; }
    public Uri Iri { get; }
    public ReferenceSource Source { get; }

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
