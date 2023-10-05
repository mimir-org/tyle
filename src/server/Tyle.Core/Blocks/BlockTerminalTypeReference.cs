using Tyle.Core.Common;
using Tyle.Core.Terminals;

namespace Tyle.Core.Blocks;

public class BlockTerminalTypeReference : HasCardinality
{
    public Guid BlockId { get; set; }
    public BlockType Block { get; set; } = null!;
    public Guid TerminalId { get; set; }
    public TerminalType Terminal { get; set; } = null!;
    public Direction Direction { get; set; }
}