namespace Mimirorg.TypeLibrary.Models.Client;

public class BlockTypeView : ImfTypeView
{
    //public int CompanyId { get; set; }
    //public string CompanyName { get; set; }
    //public State State { get; set; }
    public ICollection<ClassifierReference> Classifiers { get; set; }
    public PurposeReference? Purpose { get; set; }
    public string? Notation { get; set; }
    public string? Symbol { get; set; }
    public Aspect Aspect { get; set; }
    public ICollection<TerminalTypeReferenceView> Terminals { get; set; }
    public ICollection<AttributeTypeReferenceView> Attributes { get; set; }
}