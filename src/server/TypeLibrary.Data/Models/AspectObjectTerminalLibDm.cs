using Mimirorg.TypeLibrary.Enums;

namespace TypeLibrary.Data.Models;

public class AspectObjectTerminalLibDm
{
    public int Id { get; set; }
    public int MinQuantity { get; set; }
    public int MaxQuantity { get; set; }
    public ConnectorDirection ConnectorDirection { get; set; }
    public int NodeId { get; set; }
    public AspectObjectLibDm AspectObject { get; set; }
    public int TerminalId { get; set; }
    public TerminalLibDm Terminal { get; set; }
}