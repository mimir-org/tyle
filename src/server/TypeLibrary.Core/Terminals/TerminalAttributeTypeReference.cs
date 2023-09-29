using TypeLibrary.Core.Attributes;
using TypeLibrary.Core.Common;

namespace TypeLibrary.Core.Terminals;

public class TerminalAttributeTypeReference : HasCardinality
{
    public Guid TerminalId { get; set; }
    public TerminalType Terminal { get; set; } = null!;
    public Guid AttributeId { get; set; }
    public AttributeType Attribute { get; set; } = null!;
}