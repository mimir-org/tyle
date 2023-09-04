using Mimirorg.TypeLibrary.Enums;

namespace Mimirorg.TypeLibrary.Models.Client;

public class BlockTerminalLibCm
{
    public int Id { get; set; }
    public int MinCount { get; set; }
    public int? MaxCount { get; set; }
    public Direction Direction { get; set; }
    public TerminalLibCm Terminal { get; set; }
    public string Kind => nameof(BlockTerminalLibCm);
}