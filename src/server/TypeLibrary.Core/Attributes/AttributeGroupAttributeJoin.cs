namespace TypeLibrary.Core.Attributes;

public class AttributeGroupAttributeJoin
{
    public Guid AttributeGroupId { get; set; }
    public AttributeGroup AttributeGroup { get; set; } = null!;
    public Guid AttributeId { get; set; }
    public AttributeType Attribute { get; set; } = null!;
}