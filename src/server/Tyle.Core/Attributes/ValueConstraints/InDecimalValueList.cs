using Tyle.Core.Common;

namespace Tyle.Core.Attributes.ValueConstraints;

public class InDecimalValueList : HasCardinality, IValueConstraint
{
    public ICollection<decimal> ValueList { get; set; }

    /// <summary>
    /// Creates a new In constraint with decimal values.
    /// </summary>
    /// <param name="valueList">The list of decimal values that the attribute type can have.</param>
    /// <param name="minCount">The minimum number of values for this attribute type. Can be zero.</param>
    /// <param name="maxCount">The maximum number of values for this attribute type. Can be omitted.</param>
    /// <exception cref="ArgumentException">Thrown if the given value list contains less than two values,
    /// when the minimum count is less than zero, or when the maximum count is smaller than the minimum count.</exception>
    public InDecimalValueList(ICollection<decimal> valueList, int minCount, int? maxCount = null) : base(minCount, maxCount)
    {
        if (valueList.Count < 2)
        {
            throw new ArgumentException($"The list of values must contain at least two values.", nameof(valueList));
        }

        ValueList = valueList;
    }
}