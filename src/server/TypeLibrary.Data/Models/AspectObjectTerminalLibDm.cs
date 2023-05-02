using Mimirorg.TypeLibrary.Enums;
using VDS.RDF;

namespace TypeLibrary.Data.Models;

public class AspectObjectTerminalLibDm
{
    public string Id { get; set; }
    public int MinQuantity { get; set; }
    public int MaxQuantity { get; set; }
    public ConnectorDirection ConnectorDirection { get; set; }
    public string AspectObjectId { get; set; }
    public AspectObjectLibDm AspectObject { get; set; }
    public string TerminalId { get; set; }
    public TerminalLibDm Terminal { get; set; }

    public string GetHash()
    {
        return $"{MinQuantity}-{MaxQuantity}-{ConnectorDirection}-{TerminalId}".GetSha256Hash();
    }
}