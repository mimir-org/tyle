using Tyle.Core.Common;
using Tyle.Core.Terminals;

namespace Tyle.Core.Blocks;

public class TerminalTypeReference : HasCardinality
{
    public Direction Direction { get; }
    public TerminalType Terminal { get; }

    /// <summary>
    /// Creates a type reference from a block type to a terminal type.
    /// </summary>
    /// <param name="terminal">The terminal type that the block type should have.</param>
    /// <param name="direction">The direction of the terminal.</param>
    /// <param name="minCount">The minimum number of this terminal the block can have. Can be zero.</param>
    /// <param name="maxCount">The maximum number of this terminal the block can have. Can be omitted.</param>
    /// <exception cref="ArgumentException">Thrown when a terminal with qualifier input or output is used as
    /// an output or input terminal, respectively. Also thrown when the minimum count is less than zero, or when
    /// the maximum count is smaller than the minimum count.</exception>
    public TerminalTypeReference(TerminalType terminal, Direction direction, int minCount, int? maxCount = null) : base(minCount, maxCount)
    {
        if (terminal.Qualifier != Direction.Bidirectional && direction != terminal.Qualifier)
        {
            throw new ArgumentException($"A terminal with qualifier {terminal.Qualifier} can't be an {direction} terminal.", nameof(direction));
        }

        Terminal = terminal;
        Direction = direction;
    }
}