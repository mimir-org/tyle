namespace Tyle.Core.Blocks;

public class ConnectionPoint
{
    public int Id { get; set; }
    public int SymbolId { get; set; }
    public EngineeringSymbol Symbol { get; set; } = null!;
    public required string Identifier { get; set; }
    public required decimal PositionX { get; set; }
    public required decimal PositionY { get; set; }
}