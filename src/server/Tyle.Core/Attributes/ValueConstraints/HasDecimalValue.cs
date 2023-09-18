namespace Tyle.Core.Attributes.ValueConstraints;

public class HasDecimalValue : IValueConstraint
{
    public decimal Value { get; }

    /// <summary>
    /// Creates a new HasValue constraint with a decimal value.
    /// </summary>
    /// <param name="value">The decimal value that the attribute type must have.</param>
    public HasDecimalValue(decimal value)
    {
        Value = value;
    }
}
