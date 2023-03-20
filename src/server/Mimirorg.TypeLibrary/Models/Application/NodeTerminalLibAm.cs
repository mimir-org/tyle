using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Extensions;
using TypeScriptBuilder;

namespace Mimirorg.TypeLibrary.Models.Application;

public class NodeTerminalLibAm
{
    public int TerminalId { get; set; }
    public int MinQuantity { get; set; }
    public int MaxQuantity { get; set; }
    public ConnectorDirection ConnectorDirection { get; set; }
}