using Tyle.Api.Common;
using Tyle.Core.Common;
using Tyle.Core.Terminals;

namespace Tyle.Api.Terminals;

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
    public string Kind => nameof(TerminalView);
}