namespace Mimirorg.TypeLibrary.Models.Client;

public class BlockAttributeLibCm
{
    public Guid Id { get; set; }
    public int MinCount { get; set; }
    public int? MaxCount { get; set; }
    public AttributeLibCm Attribute { get; set; }
    public string Kind => nameof(BlockAttributeLibCm);
}
