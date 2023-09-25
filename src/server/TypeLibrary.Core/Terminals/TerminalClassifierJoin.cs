using TypeLibrary.Core.Common;

namespace TypeLibrary.Core.Terminals;

public class TerminalClassifierJoin
{
    public Guid TerminalId { get; set; }
    public TerminalType Terminal { get; set; }
    public int ClassifierId { get; set; }
    public RdlClassifier Classifier { get; set; }
}