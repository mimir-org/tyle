using System.Globalization;
using Mimirorg.Common.Contracts;

namespace Mimirorg.Common.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<TSource> Exclude<TSource, TKey>(this IEnumerable<TSource> source, IEnumerable<TSource> exclude, Func<TSource, TKey> keySelector)
        {
            var excludedSet = new HashSet<TKey>(exclude.Select(keySelector));
            return source.Where(item => !excludedSet.Contains(keySelector(item)));
        }

        public static IDictionary<TKey, TValue> Merge<TKey, TValue>(this IDictionary<TKey, TValue> dictA, IDictionary<TKey, TValue> dictB) where TValue : class
        {
            return dictA.Keys.Union(dictB.Keys).ToDictionary(k => k, k => dictA.ContainsKey(k) ? dictA[k] : dictB[k]);
        }

        public static IEnumerable<T> LatestVersion<T>(this IEnumerable<T> collection) where T : IVersionObject
        {
            return collection.OrderByDescending(x => double.Parse(x.Version, CultureInfo.InvariantCulture)).DistinctBy(x => x.FirstVersionId);
        }
    }
}