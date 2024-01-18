using Tyle.Api.Attributes;
using Tyle.Core.Common;

namespace Tyle.Api.Common;

public class AttributeTypeReferenceView : HasCardinality
{
    public required AttributeView Attribute { get; set; }
}