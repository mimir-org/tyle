using Tyle.Core.Attributes;
using Tyle.Core.Common;

namespace Tyle.Core.Blocks;

public class BlockAttributeTypeReference : HasCardinality
{
    public Guid BlockId { get; set; }
    public BlockType Block { get; set; } = null!;
    public Guid AttributeId { get; set; }
    public AttributeType Attribute { get; set; } = null!;
    public Guid? AttributeGroupId { get; set; }
    public AttributeGroup? AsPartOfAttributeGroup { get; set; }
}