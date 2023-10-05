using Tyle.Core.Blocks;

namespace Tyle.Persistence.Blocks;

public class BlockAttributeComparer : IEqualityComparer<BlockAttributeTypeReference>
{
    public bool Equals(BlockAttributeTypeReference? x, BlockAttributeTypeReference? y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (ReferenceEquals(x, null)) return false;
        if (ReferenceEquals(y, null)) return false;
        if (x.GetType() != y.GetType()) return false;

        return x.BlockId.Equals(y.BlockId) &&
               x.AttributeId.Equals(y.AttributeId) &&
               x.MinCount == y.MinCount &&
               x.MaxCount == y.MaxCount;
    }

    public int GetHashCode(BlockAttributeTypeReference obj)
    {
        return HashCode.Combine(obj.BlockId, obj.AttributeId, obj.MinCount, obj.MaxCount);
    }
}