namespace Tyle.Core.Attributes.ValueConstraints;

public class InDecimalValueList : IValueConstraint
{
    public ICollection<decimal> ValueList { get; }

    /// <summary>
    /// Creates a new In constraint with decimal values.
    /// </summary>
    /// <param name="valueList">The list of decimal values that the attribute type can have.</param>
    /// <exception cref="ArgumentException">Thrown if the given value list contains less than two values.</exception>
    public InDecimalValueList(ICollection<decimal> valueList)
    {
        if (valueList.Count < 2)
        {
            throw new ArgumentException($"The list of values must contain at least two values.", nameof(valueList));
        }

        ValueList = valueList;
    }
}