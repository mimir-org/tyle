namespace Tyle.Core.Blocks;

public class EngineeringSymbol
{
    public int Id { get; set; }
    public string Label { get; set; }
    public string Iri { get; set; }
    public string? Description { get; set; }
    public string Path { get; set; }
    public decimal Height { get; set; }
    public decimal Width { get; set; }
    public ICollection<ConnectionPoint> ConnectionPoints { get; set; } = new List<ConnectionPoint>();
}