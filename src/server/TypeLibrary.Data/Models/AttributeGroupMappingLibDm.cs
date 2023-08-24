namespace TypeLibrary.Data.Models;

public class AttributeGroupMappingLibDm
{
    public int Id { get; set; }
    public string AttributeId { get; set; }
    public AttributeLibDm Attribute { get; set;  }
    public string AttributeGroupId { get; set; }
    public AttributeGroupLibDm AttributeGroup { get; set; }
}
