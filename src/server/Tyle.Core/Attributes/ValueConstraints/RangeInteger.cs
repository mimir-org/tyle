namespace Tyle.Core.Attributes.ValueConstraints;

public class RangeInteger : IValueConstraint
{
    public int? MinValue { get; }
    public int? MaxValue { get; }
    public bool? MinInclusive { get; }
    public bool? MaxInclusive { get; }

    public RangeInteger(int? minValue, int? maxValue, bool? minInclusive, bool? maxInclusive)
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