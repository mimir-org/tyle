using Tyle.Api.Terminals;
using Tyle.Core.Blocks;
using Tyle.Core.Common;
using Tyle.Core.Terminals;

namespace Tyle.Api.Blocks;

public class TerminalTypeReferenceView : HasCardinality
{
    public Direction Direction { get; set; }
    public required TerminalView Terminal { get; set; }
    public ConnectionPoint? ConnectionPoint { get; set; }
}