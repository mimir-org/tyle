namespace Tyle.Core.Common;

public abstract class RdlObject
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required Uri Iri { get; set; }
    public ReferenceSource Source { get; set; }
}