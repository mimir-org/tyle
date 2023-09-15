using Tyle.Core.Attributes;
using Tyle.Core.Common.Exceptions;

namespace Tyle.Core.Blocks;

public class BlockAttributeTypeReference
{
    public int Id { get; set; }
    public int MinCount { get; set; }
    public int? MaxCount { get; set; }
    public Guid BlockId { get; set; }
    public BlockType Block { get; set; } = null!;
    public Guid AttributeId { get; set; }
    public AttributeType Attribute { get; set; } = null!;

    public BlockAttributeTypeReference(Guid blockId, Guid attributeId, int minCount, int? maxCount = null)
    {
        if (minCount < 0) throw new MimirorgBadRequestException("The attribute min count cannot be negative.");

        if (minCount > maxCount)
            throw new MimirorgBadRequestException(
                "The attribute min count cannot be larger than the attribute max count.");

        BlockId = blockId;
        AttributeId = attributeId;
        MinCount = minCount;
        MaxCount = maxCount;
    }
}