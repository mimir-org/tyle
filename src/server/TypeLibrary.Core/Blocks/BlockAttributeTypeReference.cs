using TypeLibrary.Core.Attributes;
using TypeLibrary.Core.Common;

namespace TypeLibrary.Core.Blocks;

public class BlockAttributeTypeReference : HasCardinality
{
    public Guid BlockId { get; set; }
    public BlockType Block { get; set; } = null!;
    public Guid AttributeId { get; set; }
    public AttributeType Attribute { get; set; } = null!;
}