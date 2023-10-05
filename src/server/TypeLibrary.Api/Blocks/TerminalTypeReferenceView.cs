using TypeLibrary.Api.Terminals;
using TypeLibrary.Core.Common;
using TypeLibrary.Core.Terminals;

namespace TypeLibrary.Api.Blocks;

public class TerminalTypeReferenceView : HasCardinality
{
    public Direction Direction { get; set; }
    public required TerminalView Terminal { get; set; }
}