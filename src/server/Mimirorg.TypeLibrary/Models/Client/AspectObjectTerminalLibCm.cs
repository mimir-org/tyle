using Mimirorg.TypeLibrary.Enums;

namespace Mimirorg.TypeLibrary.Models.Client;

public class AspectObjectTerminalLibCm
{
    public string Id { get; set; }
    public int MinQuantity { get; set; }
    public int MaxQuantity { get; set; }
    public ConnectorDirection ConnectorDirection { get; set; }
    public TerminalLibCm Terminal { get; set; }
    public string Kind => nameof(AspectObjectTerminalLibCm);
}