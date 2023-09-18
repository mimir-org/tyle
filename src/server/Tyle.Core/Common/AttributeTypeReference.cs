using Tyle.Core.Attributes;

namespace Tyle.Core.Common;

public class AttributeTypeReference : HasCardinality
{
    public AttributeType Attribute { get; }

    public AttributeTypeReference(AttributeType attribute, int minCount, int? maxCount = null) : base(minCount, maxCount)
    {
        Attribute = attribute;
    }
}