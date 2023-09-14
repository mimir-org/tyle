using Tyle.Core.Common.Domain;

namespace Tyle.Core.Terminals.Domain;

public class TerminalType : ImfType
{
    public ICollection<TerminalClassifierMapping> Classifiers { get; set; }
    public int? PurposeId { get; set; }
    public PurposeReference? Purpose { get; set; }
    public string? Notation { get; set; }
    public string? Symbol { get; set; }
    public Aspect? Aspect { get; set; }
    public int? MediumId { get; set; }
    public MediumReference? Medium { get; set; }
    public Direction Qualifier { get; set; }
    public ICollection<TerminalAttributeTypeReference> TerminalAttributes { get; set; }

    public TerminalType(string name, string? description, string createdBy) : base(name, description, createdBy)
    {
        Classifiers = new List<TerminalClassifierMapping>();
        TerminalAttributes = new List<TerminalAttributeTypeReference>();
    }
}