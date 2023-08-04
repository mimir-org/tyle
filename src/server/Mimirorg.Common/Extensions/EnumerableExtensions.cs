using Mimirorg.Common.Contracts;
using Mimirorg.Common.Enums;
using System.Globalization;

namespace Mimirorg.Common.Extensions;

public static class EnumerableExtensions
{
    public static IEnumerable<TSource> Exclude<TSource, TKey>(this IEnumerable<TSource> source, IEnumerable<TSource> exclude, Func<TSource, TKey> keySelector)
    {
        var excludedSet = new HashSet<TKey>(exclude.Select(keySelector));
        return source.Where(item => !excludedSet.Contains(keySelector(item)));
    }

    public static IDictionary<TKey, TValue> Merge<TKey, TValue>(this IDictionary<TKey, TValue> dictA, IDictionary<TKey, TValue> dictB) where TValue : class
    {
        return dictA.Keys.Union(dictB.Keys).ToDictionary(k => k, k => dictA.TryGetValue(k, out var value) ? value : dictB[k]);
    }

    public static IEnumerable<T> LatestVersions<T>(this IEnumerable<T> collection) where T : IVersionObject
    {
        return collection
            .OrderByDescending(x => VersionToDouble(x.Version))
            .DistinctBy(x => x.FirstVersionId);
    }

    public static IEnumerable<T> LatestVersionsApproved<T>(this IEnumerable<T> collection) where T : IVersionObject
    {
        return collection
            .Where(x => x.State == State.Approved)
            .OrderByDescending(x => VersionToDouble(x.Version))
            .DistinctBy(x => x.FirstVersionId);
    }

    public static T LatestVersion<T>(this IEnumerable<T> collection, string firstVersionId) where T : IVersionObject
    {
        return collection
            .Where(x => x.FirstVersionId == firstVersionId)
            .OrderByDescending(x => VersionToDouble(x.Version))
            .FirstOrDefault();
    }

    public static T LatestVersionApproved<T>(this IEnumerable<T> collection, string firstVersionId) where T : IVersionObject
    {
        return collection
            .Where(x => x.FirstVersionId == firstVersionId && x.State == State.Approved)
            .OrderByDescending(x => VersionToDouble(x.Version))
            .FirstOrDefault();
    }

    private static double VersionToDouble(string version)
    {
        var split = version.Split(".");
        return double.Parse(split[1].Length == 1 ? $"{split[0]}.0{split[1]}" : $"{split[0]}.{split[1]}", CultureInfo.InvariantCulture);
    }
}