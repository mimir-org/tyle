using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TypeLibrary.Data.Terminals;

[Table("Terminal_Classifier")]
[PrimaryKey(nameof(TerminalId), nameof(ClassifierId))]
public class TerminalClassifierDao
{
    public Guid TerminalId { get; set; }
    public TerminalDao Terminal { get; set; } = null!;

    public int ClassifierId { get; set; }
    public ClassifierDao Classifier { get; set; } = null!;

    public TerminalClassifierDao(Guid terminalId, int classifierId)
    {
        TerminalId = terminalId;
        ClassifierId = classifierId;
    }
}