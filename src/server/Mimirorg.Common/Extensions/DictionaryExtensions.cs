namespace Mimirorg.Common.Extensions;

public static class DictionaryExtensions
{
    public static bool ContentEquals<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, Dictionary<TKey, TValue> otherDictionary)
    {
        if (dictionary == null && otherDictionary == null)
            return true;

        if (dictionary != null && otherDictionary == null)
            return false;

        return dictionary?.Count == otherDictionary.Count && otherDictionary.OrderBy(kvp => kvp.Key).SequenceEqual((dictionary).OrderBy(kvp => kvp.Key));
    }
}