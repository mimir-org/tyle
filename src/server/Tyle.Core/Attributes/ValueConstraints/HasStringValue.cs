namespace Tyle.Core.Attributes.ValueConstraints;

public class HasStringValue : IValueConstraint
{
    public string Value { get; }

    /// <summary>
    /// Creates a new HasValue constraint with a string value.
    /// </summary>
    /// <param name="value">The string value that the attribute type must have.</param>
    public HasStringValue(string value)
    {
        Value = value;
    }
}
