namespace TypeLibrary.Data.Models;

public class BlockAttributeLibDm
{
    public int Id { get; set; }
    public int MinCount { get; set; }
    public int MaxCount { get; set; }
    public string BlockId { get; set; }
    public BlockLibDm Block { get; set; }
    public string AttributeId { get; set; }
    public AttributeLibDm Attribute { get; set; }
}