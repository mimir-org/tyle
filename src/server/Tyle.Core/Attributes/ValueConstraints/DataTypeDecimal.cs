using Tyle.Core.Common;

namespace Tyle.Core.Attributes.ValueConstraints;

public class DataTypeDecimal : HasCardinality, IValueConstraint
{
    /// <summary>
    /// Creates a new decimal data type constraint.
    /// </summary>
    /// <param name="minCount">The minimum number of values for this attribute type. Can be zero.</param>
    /// <param name="maxCount">The maximum number of values for this attribute type. Can be omitted.</param>
    /// <exception cref="ArgumentException">Thrown when the minimum count is less than zero, or when
    /// the maximum count is smaller than the minimum count.</exception>
    public DataTypeDecimal(int minCount, int? maxCount = null) : base(minCount, maxCount)
    {
    }
}