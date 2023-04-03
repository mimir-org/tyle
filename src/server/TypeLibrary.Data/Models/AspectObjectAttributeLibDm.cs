namespace TypeLibrary.Data.Models;

public class AspectObjectAttributeLibDm
{
    public int Id { get; set; }
    public int AspectObjectId { get; set; }
    public AspectObjectLibDm AspectObject { get; set; }
    public int AttributeId { get; set; }
    public AttributeLibDm Attribute { get; set; }
}