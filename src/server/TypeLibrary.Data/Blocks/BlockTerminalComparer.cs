using TypeLibrary.Core.Blocks;

namespace TypeLibrary.Data.Blocks;

public class BlockTerminalComparer : IEqualityComparer<BlockTerminalTypeReference>
{
    public bool Equals(BlockTerminalTypeReference? x, BlockTerminalTypeReference? y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (ReferenceEquals(x, null)) return false;
        if (ReferenceEquals(y, null)) return false;
        if (x.GetType() != y.GetType()) return false;

        return x.BlockId.Equals(y.BlockId) &&
               x.TerminalId.Equals(y.TerminalId) &&
               x.Direction == y.Direction &&
               x.MinCount == y.MinCount &&
               x.MaxCount == y.MaxCount;
    }

    public int GetHashCode(BlockTerminalTypeReference obj)
    {
        return HashCode.Combine(obj.BlockId, obj.TerminalId, (int) obj.Direction, obj.MinCount, obj.MaxCount);
    }
}