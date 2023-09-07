using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Mimirorg.Common.Converters;

public class StringCollectionValueConverter : ValueConverter<ICollection<string>, string>
{
    public StringCollectionValueConverter() : base(
        v => string.Join(",", v.Select(s => s.Trim())),
        v => new HashSet<string>(v.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)))
    {

    }
}

public class StringCollectionValueComparer : ValueComparer<ICollection<string>>
{
    public StringCollectionValueComparer() : base((c1, c2) => c1.SequenceEqual(c2),
        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())), c => c.ToHashSet())
    {
    }
}

public class StringHashSetValueConverter : ValueConverter<HashSet<string>, string>
{
    public StringHashSetValueConverter() : base(
        v => string.Join(",", v.Select(s => s.Trim())),
        v => new HashSet<string>(v.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)))
    {

    }
}

public class StringHashSetValueComparer : ValueComparer<HashSet<string>>
{
    public StringHashSetValueComparer() : base((c1, c2) => c1.SequenceEqual(c2),
        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())), c => c.ToHashSet())
    {
    }
}