using TypeLibrary.Api.Common;
using TypeLibrary.Core.Common;
using TypeLibrary.Core.Terminals;

namespace TypeLibrary.Api.Terminals;

public class TerminalView : ImfType
{
    public ICollection<RdlClassifier> Classifiers { get; set; } = new List<RdlClassifier>();
    public RdlPurpose? Purpose { get; set; }
    public string? Notation { get; set; }
    public string? Symbol { get; set; }
    public Aspect? Aspect { get; set; }
    public RdlMedium? Medium { get; set; }
    public Direction Qualifier { get; set; }
    public ICollection<AttributeTypeReferenceView> Attributes { get; set; } = new List<AttributeTypeReferenceView>();
}