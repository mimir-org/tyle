namespace Mimirorg.TypeLibrary.Models.Client;

public class TerminalAttributeLibCm
{
    public int Id { get; set; }
    public AttributeLibCm Attribute { get; set; }
    public string Kind => nameof(TerminalAttributeLibCm);
}