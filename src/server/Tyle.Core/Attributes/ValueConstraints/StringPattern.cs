using Tyle.Core.Common;

namespace Tyle.Core.Attributes.ValueConstraints;

public class StringPattern : HasCardinality, IValueConstraint
{
    public string Pattern { get; set; }

    /// <summary>
    /// Creates a new string pattern constraint.
    /// </summary>
    /// <param name="pattern">The string representing the pattern that string values must adhere to.</param>
    /// <param name="minCount">The minimum number of values for this attribute type. Can be zero.</param>
    /// <param name="maxCount">The maximum number of values for this attribute type. Can be omitted.</param>
    /// <exception cref="ArgumentException">Thrown when the minimum count is less than zero, or when
    /// the maximum count is smaller than the minimum count.</exception>
    public StringPattern(string pattern, int minCount, int? maxCount = null) : base(minCount, maxCount)
    {
        Pattern = pattern;
    }
}
