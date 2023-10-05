using Tyle.Core.Attributes;
using Tyle.Core.Common;

namespace Tyle.Core.Terminals;

public class TerminalAttributeTypeReference : HasCardinality
{
    public Guid TerminalId { get; set; }
    public TerminalType Terminal { get; set; } = null!;
    public Guid AttributeId { get; set; }
    public AttributeType Attribute { get; set; } = null!;
    public Guid? AttributeGroupId { get; set; }
    public AttributeGroup? AsPartOfAttributeGroup { get; set; }
}