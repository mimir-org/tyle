using Tyle.Application.Common.Views;
using Tyle.Core.Common;
using Tyle.Core.Terminals;

namespace Tyle.Application.Terminals.Views;

public class TerminalTypeView : ImfTypeView
{
    //public State State { get; set; }
    public ICollection<ClassifierReference> Classifiers { get; set; }
    public PurposeReference? Purpose { get; set; }
    public string? Notation { get; set; }
    public string? Symbol { get; set; }
    public Aspect? Aspect { get; set; }
    public MediumReference? Medium { get; set; }
    public Direction Qualifier { get; set; }
    public ICollection<AttributeTypeReferenceView> Attributes { get; set; }
}