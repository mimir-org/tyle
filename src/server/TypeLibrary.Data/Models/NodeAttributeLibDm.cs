namespace TypeLibrary.Data.Models;

public class NodeAttributeLibDm
{
    public int Id { get; set; }
    public int NodeId { get; set; }
    public NodeLibDm Node { get; set; }
    public int AttributeId { get; set; }
    public AttributeLibDm Attribute { get; set; }
}