namespace Tyle.Core.Attributes.ValueConstraints;

public class HasIntegerValue
{
    public int Value { get; }

    /// <summary>
    /// Creates a new HasValue constraint with an integer value.
    /// </summary>
    /// <param name="value">The integer value that the attribute type must have.</param>
    public HasIntegerValue(int value)
    {
        Value = value;
    }
}
