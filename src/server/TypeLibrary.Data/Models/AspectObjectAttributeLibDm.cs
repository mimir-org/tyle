namespace TypeLibrary.Data.Models;

public class AspectObjectAttributeLibDm
{
    public string Id { get; set; }
    public string AspectObjectId { get; set; }
    public AspectObjectLibDm AspectObject { get; set; }
    public string AttributeId { get; set; }
    public AttributeLibDm Attribute { get; set; }
}