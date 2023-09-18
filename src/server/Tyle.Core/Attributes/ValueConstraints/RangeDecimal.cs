namespace Tyle.Core.Attributes.ValueConstraints;

public class RangeDecimal : CardinalityConstraint, IValueConstraint
{
    public decimal? MinValue { get; }
    public decimal? MaxValue { get; }
    public bool? MinInclusive { get; }
    public bool? MaxInclusive { get; }

    /// <summary>
    /// Creates a new decimal range constraint.
    /// </summary>
    /// <param name="minValue">The lower bound. Can be null.</param>
    /// <param name="maxValue">The upper bound. Can be null.</param>
    /// <param name="minInclusive">Set to true if the lower bound is inclusive, false if it is exclusive.</param>
    /// <param name="maxInclusive">Set to true if the upper bound is inclusive, false if it is exclusive.</param>
    /// <param name="minCount">The minimum number of values for this attribute type. Can be zero.</param>
    /// <param name="maxCount">The maximum number of values for this attribute type. Can be omitted.</param>
    /// <exception cref="ArgumentException">Thrown when both upper and lower bounds are null, when the
    /// inclusive/exclusive parameter is not given for a bound, when the upper bound is larger than the lower
    /// bound, when the minimum count is less than zero, or when the maximum count is smaller than the minimum count.</exception>
    public RangeDecimal(decimal? minValue, decimal? maxValue, bool? minInclusive, bool? maxInclusive, int minCount, int? maxCount) : base(minCount, maxCount)
    {
        if (minValue == null && maxValue == null)
        {
            throw new ArgumentException("At least one bound (upper or lower) must be set.");
        }

        if (minValue != null && minInclusive == null)
        {
            throw new ArgumentException("You must specify inclusive or exclusive when setting a lower bound.", nameof(minInclusive));
        }

        if (maxValue != null && maxInclusive == null)
        {
            throw new ArgumentException("You must specify inclusive or exclusive when setting an upper bound.", nameof(maxInclusive));
        }

        if (minValue >= maxValue)
        {
            throw new ArgumentException("The upper bound must be larger than the lower bound.", nameof(maxValue));
        }

        MinValue = minValue;
        MaxValue = maxValue;
        MinInclusive = minInclusive;
        MaxInclusive = maxInclusive;
    }
}
