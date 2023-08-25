using System;

namespace TypeLibrary.Data.Models;

public class AttributeGroupMappingLibDm
{
    public Guid Id { get; set; }
    public Guid AttributeId { get; set; }
    public AttributeLibDm Attribute { get; set;  }
    public Guid AttributeGroupId { get; set; }
    public AttributeGroupLibDm AttributeGroup { get; set; }
}
