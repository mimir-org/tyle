namespace Mimirorg.TypeLibrary.Models.Client;

public class AspectObjectAttributeLibCm
{
    public string Id { get; set; }
    public AttributeLibCm Attribute { get; set; }
    public string Kind => nameof(AspectObjectAttributeLibCm);
}