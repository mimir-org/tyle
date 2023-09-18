namespace Tyle.Core.Attributes.ValueConstraints;

public class InIriValueList : IValueConstraint
{
    public ICollection<Uri> ValueList { get; }

    /// <summary>
    /// Creates a new In constraint with IRI values.
    /// </summary>
    /// <param name="valueList">The list of IRI values that the attribute type can have.</param>
    /// <exception cref="ArgumentException">Thrown if the given value list contains less than two values,
    /// or if one of the given IRIs is not an absolute URI.</exception>
    public InIriValueList(ICollection<Uri> valueList)
    {
        if (valueList.Count < 2)
        {
            throw new ArgumentException($"The list of values must contain at least two values.", nameof(valueList));
        }

        foreach (var value in valueList)
        {
            if (!value.IsAbsoluteUri)
            {
                throw new ArgumentException($"{value} is not a valid IRI.", nameof(valueList));
            }
        }

        ValueList = valueList;
    }
}