using TypeLibrary.Core.Common;
using TypeLibrary.Core.Terminals;

namespace TypeLibrary.Api.Terminals;

public class TerminalTypeView : ImfType
{
    public ICollection<RdlClassifier> Classifiers { get; set; }
    public RdlPurpose? Purpose { get; set; }
    public string? Notation { get; set; }
    public string? Symbol { get; set; }
    public Aspect? Aspect { get; set; }
    public RdlMedium? Medium { get; set; }
    public Direction Qualifier { get; set; }
    public ICollection<TerminalAttributeTypeReference> Attributes { get; set; }
}
