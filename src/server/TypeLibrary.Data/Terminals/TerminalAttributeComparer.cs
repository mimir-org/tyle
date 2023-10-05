using TypeLibrary.Core.Terminals;

namespace TypeLibrary.Data.Terminals;

public class TerminalAttributeComparer : IEqualityComparer<TerminalAttributeTypeReference>
{
    public bool Equals(TerminalAttributeTypeReference? x, TerminalAttributeTypeReference? y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (ReferenceEquals(x, null)) return false;
        if (ReferenceEquals(y, null)) return false;
        if (x.GetType() != y.GetType()) return false;

        return x.TerminalId.Equals(y.TerminalId) &&
               x.AttributeId.Equals(y.AttributeId) &&
               x.MinCount == y.MinCount &&
               x.MaxCount == y.MaxCount;
    }

    public int GetHashCode(TerminalAttributeTypeReference obj)
    {
        return HashCode.Combine(obj.TerminalId, obj.AttributeId, obj.MinCount, obj.MaxCount);
    }
}