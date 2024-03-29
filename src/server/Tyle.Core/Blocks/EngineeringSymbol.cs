namespace Tyle.Core.Blocks;

public class EngineeringSymbol
{
    public int Id { get; set; }
    public required string Label { get; set; }
    public required Uri Iri { get; set; }
    public string? Description { get; set; }
    public required string Path { get; set; }
    public decimal Height { get; set; }
    public decimal Width { get; set; }
    public ICollection<ConnectionPoint> ConnectionPoints { get; set; } = new List<ConnectionPoint>();
}