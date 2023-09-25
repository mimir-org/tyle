using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TypeLibrary.Data.Attributes;

namespace TypeLibrary.Data.Terminals;

[Table("Terminal_Attribute")]
public class TerminalAttributeDao
{
    public int Id { get; set; }

    public Guid TerminalId { get; set; }
    public TerminalDao Terminal { get; set; } = null!;

    public Guid AttributeId { get; set; }
    public AttributeDao Attribute { get; set; } = null!;

    [Required]
    public int MinCount { get; set; }

    public int? MaxCount { get; set; }

    public TerminalAttributeDao(Guid terminalId, Guid attributeId, int minCount, int? maxCount = null)
    {
        TerminalId = terminalId;
        AttributeId = attributeId;
        MinCount = minCount;
        MaxCount = maxCount;
    }
}