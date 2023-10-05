using TypeLibrary.Api.Attributes;
using TypeLibrary.Core.Common;

namespace TypeLibrary.Api.Common;

public class AttributeTypeReferenceView : HasCardinality
{
    public required AttributeView Attribute { get; set; }
}