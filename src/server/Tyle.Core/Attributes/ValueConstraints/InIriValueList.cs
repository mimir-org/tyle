namespace Tyle.Core.Attributes.ValueConstraints;

public class InIriValueList : CardinalityConstraint, IValueConstraint
{
    public ICollection<Uri> ValueList { get; }

    /// <summary>
    /// Creates a new In constraint with IRI values.
    /// </summary>
    /// <param name="valueList">The list of IRI values that the attribute type can have.</param>
    /// <param name="minCount">The minimum number of values for this attribute type. Can be zero.</param>
    /// <param name="maxCount">The maximum number of values for this attribute type. Can be omitted.</param>
    /// <exception cref="ArgumentException">Thrown if the given value list contains less than two values,
    /// the given IRI is not an absolute URI, when the minimum count is less than zero,
    /// or when the maximum count is smaller than the minimum count.</exception>
    public InIriValueList(ICollection<Uri> valueList, int minCount, int? maxCount = null) : base(minCount, maxCount)
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