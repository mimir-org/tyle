using Tyle.Core.Attributes;

namespace Tyle.Core.Common;

public class AttributeTypeReference : HasCardinality
{
    public AttributeType Attribute { get; }

    /// <summary>
    /// Creates a type reference from a block or terminal type to an attribute type.
    /// </summary>
    /// <param name="attribute">The attribute type that the block or terminal type should have.</param>
    /// <param name="minCount">The minimum number of this attribute the element can have. Can be zero.</param>
    /// <param name="maxCount">The maximum number of this attribute the element can have. Can be omitted.</param>
    /// <exception cref="ArgumentException">Thrown when the minimum count is less than zero, or when
    /// the maximum count is smaller than the minimum count.</exception>
    public AttributeTypeReference(AttributeType attribute, int minCount, int? maxCount = null) : base(minCount, maxCount)
    {
        Attribute = attribute;
    }
}