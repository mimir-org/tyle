namespace Tyle.Core.Attributes.ValueConstraints;

public abstract class CardinalityConstraint
{
    public int MinCount { get; }
    public int? MaxCount { get; }

    protected CardinalityConstraint(int minCount, int? maxCount = null)
    {
        if (minCount < 0)
        {
            throw new ArgumentException("The minimum count must be a positive number.", nameof(minCount));
        }

        if (maxCount < minCount)
        {
            throw new ArgumentException($"The maximum count can't be smaller than the minimum count ({minCount}).", nameof(maxCount));
        }

        MinCount = minCount;
        MaxCount = maxCount;
    }
}
