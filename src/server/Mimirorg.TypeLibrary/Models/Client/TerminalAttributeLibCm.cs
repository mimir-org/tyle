namespace Mimirorg.TypeLibrary.Models.Client;

public class TerminalAttributeLibCm
{
    public string Id { get; set; }
    public AttributeLibCm Attribute { get; set; }
    public string Kind => nameof(TerminalAttributeLibCm);
}