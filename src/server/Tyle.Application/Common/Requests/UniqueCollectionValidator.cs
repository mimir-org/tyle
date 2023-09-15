using System.ComponentModel.DataAnnotations;

namespace Tyle.Application.Common.Requests;

public static class UniqueCollectionValidator
{
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
