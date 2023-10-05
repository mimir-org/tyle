using Tyle.Api.Common;
using Tyle.Core.Common;

namespace Tyle.Api.Blocks;

public class BlockView : ImfType
{
    public ICollection<RdlClassifier> Classifiers { get; set; } = new List<RdlClassifier>();
    public RdlPurpose? Purpose { get; set; }
    public string? Notation { get; set; }
    public string? Symbol { get; set; }
    public Aspect? Aspect { get; set; }
    public ICollection<TerminalTypeReferenceView> Terminals { get; set; } = new List<TerminalTypeReferenceView>();
    public ICollection<AttributeTypeReferenceView> Attributes { get; set; } = new List<AttributeTypeReferenceView>();
}