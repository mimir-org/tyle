namespace Tyle.Core.Attributes.ValueConstraints;

public class InStringValueList : IValueConstraint
{
    public ICollection<string> ValueList { get; }

    /// <summary>
    /// Creates a new In constraint with string values.
    /// </summary>
    /// <param name="valueList">The list of string values that the attribute type can have.</param>
    /// <exception cref="ArgumentException">Thrown if the given value list contains less than two values.</exception>
    public InStringValueList(ICollection<string> valueList)
    {
        if (valueList.Count < 2)
        {
            throw new ArgumentException($"The list of values must contain at least two values.", nameof(valueList));
        }

        ValueList = valueList;
    }
}
