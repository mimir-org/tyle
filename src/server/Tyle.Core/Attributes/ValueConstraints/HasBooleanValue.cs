namespace Tyle.Core.Attributes.ValueConstraints;

public class HasBooleanValue : IValueConstraint
{
    public bool Value { get; }

    /// <summary>
    /// Creates a new HasValue constraint with a boolean value.
    /// </summary>
    /// <param name="value">The boolean value that the attribute type must have.</param>
    public HasBooleanValue(bool value)
    {
        Value = value;
    }
}