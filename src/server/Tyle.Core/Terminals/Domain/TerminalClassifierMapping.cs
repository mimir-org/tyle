using Tyle.Core.Common.Domain;

namespace Tyle.Core.Terminals.Domain;

public class TerminalClassifierMapping
{
    public int Id { get; set; }
    public Guid TerminalId { get; set; }
    public TerminalType Terminal { get; set; } = null!;
    public int ClassifierId { get; set; }
    public ClassifierReference Classifier { get; set; } = null!;

    public TerminalClassifierMapping(Guid terminalId, int classifierId)
    {
        TerminalId = terminalId;
        ClassifierId = classifierId;
    }
}