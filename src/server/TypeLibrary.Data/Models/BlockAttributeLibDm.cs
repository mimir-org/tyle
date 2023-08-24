namespace TypeLibrary.Data.Models;

public class BlockAttributeLibDm
{
    public int Id { get; set; }
    public int MinCount { get; set; }
    public int? MaxCount { get; set; }
    public string BlockId { get; set; } = null!;
    public BlockLibDm Block { get; set; } = null!;
    public string AttributeId { get; set; } = null!;
    public AttributeLibDm Attribute { get; set; } = null!;
}