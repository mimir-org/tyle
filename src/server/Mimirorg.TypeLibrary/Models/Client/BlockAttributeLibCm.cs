namespace Mimirorg.TypeLibrary.Models.Client;

public class BlockAttributeLibCm
{
    public int Id { get; set; }
    public int MinCount { get; set; }
    public int? MaxCount { get; set; }
    public AttributeTypeView Attribute { get; set; }
    public string Kind => nameof(BlockAttributeLibCm);
}
