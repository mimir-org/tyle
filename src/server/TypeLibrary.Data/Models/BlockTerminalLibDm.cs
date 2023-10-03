using Mimirorg.TypeLibrary.Enums;
using VDS.RDF;

namespace TypeLibrary.Data.Models;

public class BlockTerminalLibDm
{
    public string Id { get; set; }
    public int MinQuantity { get; set; }
    public int MaxQuantity { get; set; }
    public ConnectorDirection ConnectorDirection { get; set; }
    public string BlockId { get; set; }
    public BlockLibDm Block { get; set; }
    public string TerminalId { get; set; }
    public TerminalLibDm Terminal { get; set; }

    public string GetHash()
    {
        return $"{MinQuantity}-{MaxQuantity}-{ConnectorDirection}-{TerminalId}".GetSha256Hash();
    }
}