namespace Tyle.Core.Attributes.ValueConstraints;

public class HasIriValue : IValueConstraint
{
    public Uri Value { get; }

    /// <summary>
    /// Creates a new HasValue constraint with an IRI value.
    /// </summary>
    /// <param name="value">The IRI value that the attribute type must have.</param>
    /// <exception cref="ArgumentException">Thrown if the given IRI is not an absolute URI.</exception>
    public HasIriValue(Uri value)
    {
        if (!value.IsAbsoluteUri)
        {
            throw new ArgumentException($"{value} is not a valid IRI.", nameof(value));
        }

        Value = value;
    }
}
