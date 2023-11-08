namespace Tyle.Core.Blocks;

public class Symbol
{
    public int Id { get; set; }
    public required string Label { get; set; }
    public required Uri Iri { get; set; }
    public string? Description { get; set; }
    public required string SvgString { get; set; }
    public ICollection<ConnectionPoint> ConnectionPoints { get; set; } = new List<ConnectionPoint>();
}