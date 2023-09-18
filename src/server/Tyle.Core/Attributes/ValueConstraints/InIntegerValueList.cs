namespace Tyle.Core.Attributes.ValueConstraints;

public class InIntegerValueList : IValueConstraint
{
    public ICollection<int> ValueList { get; }

    /// <summary>
    /// Creates a new In constraint with integer values.
    /// </summary>
    /// <param name="valueList">The list of integer values that the attribute type can have.</param>
    /// <exception cref="ArgumentException">Thrown if the given value list contains less than two values.</exception>
    public InIntegerValueList(ICollection<int> valueList)
    {
        if (valueList.Count < 2)
        {
            throw new ArgumentException($"The list of values must contain at least two values.", nameof(valueList));
        }

        ValueList = valueList;
    }
}