using Tyle.Core.Common;

namespace Tyle.Core.Terminals;

public class TerminalClassifierJoin
{
    public Guid TerminalId { get; set; }
    public TerminalType Terminal { get; set; } = null!;
    public int ClassifierId { get; set; }
    public RdlClassifier Classifier { get; set; } = null!;
}