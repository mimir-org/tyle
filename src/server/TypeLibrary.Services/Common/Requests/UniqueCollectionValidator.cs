using System.ComponentModel.DataAnnotations;

namespace TypeLibrary.Services.Common.Requests;

public static class UniqueCollectionValidator
{
    /// <summary>
    /// Checks if an IEnumerable only contains unique entries.
    /// </summary>
    /// <param name="collection">The IEnumerable that should be checked for unique entries.</param>
    /// <param name="entity">A descriptive name for the entries, to be used in error messages.</param>
    /// <returns>The method will yield validation results for each non-unique entry in the enumerable.</returns>
    public static IEnumerable<ValidationResult> Validate<T>(IEnumerable<T> collection, string entity)
    {
        var uniqueCollection = new HashSet<T>();

        foreach (var item in collection)
        {
            if (!uniqueCollection.Add(item))
                yield return new ValidationResult($"{entity} {item} is not unique.");
        }
    }
}