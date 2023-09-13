using Mimirorg.Common.Exceptions;

namespace Mimirorg.TypeLibrary.Models.Domain;

public class TerminalAttributeTypeReference
{
    public int Id { get; set; }
    public int MinCount { get; set; }
    public int? MaxCount { get; set; }
    public Guid TerminalId { get; set; }
    public TerminalType Terminal { get; set; } = null!;
    public Guid AttributeId { get; set; }
    public AttributeType Attribute { get; set; } = null!;

    public TerminalAttributeTypeReference(Guid terminalId, Guid attributeId, int minCount, int? maxCount = null)
    {
        TerminalId = terminalId;
        AttributeId = attributeId;
        MinCount = minCount;
        MaxCount = maxCount;
    }
}