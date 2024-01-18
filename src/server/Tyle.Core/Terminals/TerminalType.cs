using Tyle.Core.Common;

namespace Tyle.Core.Terminals;

public class TerminalType : ImfType
{
    public ICollection<TerminalClassifierJoin> Classifiers { get; set; } = new List<TerminalClassifierJoin>();
    public int? PurposeId { get; set; }
    public RdlPurpose? Purpose { get; set; }
    public string? Notation { get; set; }
    public string? Symbol { get; set; }
    public Aspect? Aspect { get; set; }
    public int? MediumId { get; set; }
    public RdlMedium? Medium { get; set; }
    public Direction Qualifier { get; set; }
    public ICollection<TerminalAttributeTypeReference> Attributes { get; set; } = new List<TerminalAttributeTypeReference>();
}