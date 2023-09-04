namespace Mimirorg.TypeLibrary.Models.Client;

public class TerminalAttributeLibCm
{
    public int Id { get; set; }
    public int MinCount { get; set; }
    public int? MaxCount { get; set; }
    public AttributeLibCm Attribute { get; set; }
    public string Kind => nameof(TerminalAttributeLibCm);
}