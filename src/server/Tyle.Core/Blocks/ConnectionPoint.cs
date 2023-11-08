namespace Tyle.Core.Blocks;

public class ConnectionPoint
{
    public int Id { get; set; }
    public int SymbolId { get; set; }
    public Symbol Symbol { get; set; } = null!;
    public required string Identifier { get; set; }
    public required int ConnectorDirection { get; set; }
    public required int PositionX { get; set; }
    public required int PositionY { get; set; }
}