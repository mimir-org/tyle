namespace Mimirorg.TypeLibrary.Models.Client;

public class NodeAttributeLibCm
{
    public string Id { get; set; }
    public AttributeLibCm Attribute { get; set; }
    public string Kind => nameof(NodeAttributeLibCm);
}