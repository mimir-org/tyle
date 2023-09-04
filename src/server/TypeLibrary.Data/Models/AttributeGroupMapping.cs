using System;

namespace TypeLibrary.Data.Models;

public class AttributeGroupMapping
{
    public Guid Id { get; set; }
    public Guid AttributeId { get; set; }
    public AttributeType Attribute { get; set; } = null!;
    public Guid AttributeGroupId { get; set; }
    public AttributeGroup AttributeGroup { get; set; } = null!;
}
