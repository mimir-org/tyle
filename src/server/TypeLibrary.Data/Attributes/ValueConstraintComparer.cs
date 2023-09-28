using TypeLibrary.Core.Attributes;

namespace TypeLibrary.Data.Attributes;

public class ValueConstraintComparer : IEqualityComparer<ValueConstraint>
{
    public bool Equals(ValueConstraint? x, ValueConstraint? y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (ReferenceEquals(x, null)) return false;
        if (ReferenceEquals(y, null)) return false;
        if (x.GetType() != y.GetType()) return false;

        if (!x.ValueList.Select(e => e.EntryValue).SequenceEqual(y.ValueList.Select(e => e.EntryValue)))
        {
            return false;
        }

        return x.ConstraintType == y.ConstraintType &&
               x.DataType == y.DataType &&
               x.MinCount == y.MinCount &&
               x.MaxCount == y.MaxCount &&
               x.Value == y.Value &&
               x.Pattern == y.Pattern &&
               x.MinValue == y.MinValue &&
               x.MaxValue == y.MaxValue &&
               x.MinInclusive == y.MinInclusive &&
               x.MaxInclusive == y.MaxInclusive;
    }

    public int GetHashCode(ValueConstraint obj)
    {
        var hashCode = new HashCode();
        hashCode.Add((int) obj.ConstraintType);
        hashCode.Add((int) obj.DataType);
        hashCode.Add(obj.MinCount);
        hashCode.Add(obj.MaxCount);
        hashCode.Add(obj.Value);
        foreach (var entry in obj.ValueList)
        {
            hashCode.Add(entry);
        }
        hashCode.Add(obj.Pattern);
        hashCode.Add(obj.MinValue);
        hashCode.Add(obj.MaxValue);
        hashCode.Add(obj.MinInclusive);
        hashCode.Add(obj.MaxInclusive);
        return hashCode.ToHashCode();
    }
}
