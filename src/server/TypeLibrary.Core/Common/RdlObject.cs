namespace TypeLibrary.Core.Common;

public abstract class RdlObject
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public Uri Iri { get; set; }
    public ReferenceSource Source { get; set; }
}