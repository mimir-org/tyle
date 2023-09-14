using Tyle.Core.Common.Exceptions;
using Tyle.Core.Terminals.Domain;

namespace Tyle.Core.Blocks.Domain;

public class BlockTerminalTypeReference
{
    public int Id { get; set; }
    public int MinCount { get; set; }
    public int? MaxCount { get; set; }
    public Direction Direction { get; set; }
    public Guid BlockId { get; set; }
    public BlockType Block { get; set; } = null!;
    public Guid TerminalId { get; set; }
    public TerminalType Terminal { get; set; } = null!;

    public BlockTerminalTypeReference(Guid blockId, Guid terminalId, Direction direction, int minCount, int? maxCount = null)
    {
        if (minCount < 0) throw new MimirorgBadRequestException("The terminal min count cannot be negative.");

        if (minCount > maxCount)
            throw new MimirorgBadRequestException(
                "The terminal min count cannot be larger than the terminal max count.");

        BlockId = blockId;
        TerminalId = terminalId;
        Direction = direction;
        MinCount = minCount;
        MaxCount = maxCount;
    }
}