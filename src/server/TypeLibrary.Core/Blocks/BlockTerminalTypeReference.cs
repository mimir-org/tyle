using TypeLibrary.Core.Common;
using TypeLibrary.Core.Terminals;

namespace TypeLibrary.Core.Blocks;

public class BlockTerminalTypeReference : HasCardinality
{
    public Guid BlockId { get; set; }
    public BlockType Block { get; set; }
    public Guid TerminalId { get; set; }
    public TerminalType Terminal { get; set; }
    public Direction Direction { get; set; }
}