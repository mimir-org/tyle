using Mimirorg.TypeLibrary.Enums;

namespace Mimirorg.TypeLibrary.Models.Application;

public class NodeTerminalLibAm
{
    public int MinQuantity { get; set; }
    public int MaxQuantity { get; set; }
    public ConnectorDirection ConnectorDirection { get; set; }
    public int TerminalId { get; set; }
}