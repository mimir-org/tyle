namespace TypeLibrary.Data.Models;

public class TerminalAttributeLibDm
{
    public int Id { get; set; }
    public int MinCount { get; set; }
    public int? MaxCount { get; set; }
    public string TerminalId { get; set; } = null!;
    public TerminalLibDm Terminal { get; set; } = null!;
    public string AttributeId { get; set; } = null!;
    public AttributeLibDm Attribute { get; set; } = null!;
}