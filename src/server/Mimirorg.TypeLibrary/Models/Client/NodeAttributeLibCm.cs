namespace Mimirorg.TypeLibrary.Models.Client;

public class NodeAttributeLibCm
{
    public int Id { get; set; }
    public AttributeLibCm Attribute { get; set; }
    public string Kind => nameof(NodeAttributeLibCm);
}