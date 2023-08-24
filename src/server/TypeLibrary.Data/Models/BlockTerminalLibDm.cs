using Mimirorg.TypeLibrary.Enums;
using VDS.RDF;

namespace TypeLibrary.Data.Models;

public class BlockTerminalLibDm
{
    public string Id { get; set; }
    public int MinCount { get; set; }
    public int MaxCount { get; set; }
    public Direction Direction { get; set; }
    public string BlockId { get; set; }
    public BlockLibDm Block { get; set; }
    public string TerminalId { get; set; }
    public TerminalLibDm Terminal { get; set; }

    public string GetHash()
    {
        return $"{MinCount}-{MaxCount}-{Direction}-{TerminalId}".GetSha256Hash();
    }
}