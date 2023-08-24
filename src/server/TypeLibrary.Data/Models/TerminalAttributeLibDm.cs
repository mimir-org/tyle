namespace TypeLibrary.Data.Models;

public class TerminalAttributeLibDm
{
    public int Id { get; set; }
    public int MinCount { get; set; }
    public int MaxCount { get; set; }
    public string TerminalId { get; set; }
    public TerminalLibDm Terminal { get; set; }
    public string AttributeId { get; set; }
    public AttributeLibDm Attribute { get; set; }
}