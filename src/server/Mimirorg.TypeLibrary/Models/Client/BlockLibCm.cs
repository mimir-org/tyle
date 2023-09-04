using Mimirorg.TypeLibrary.Enums;

namespace Mimirorg.TypeLibrary.Models.Client;

public class BlockLibCm
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTimeOffset CreatedOn { get; set; }
    public string CreatedBy { get; set; }
    public ICollection<string> ContributedBy { get; set; }
    public DateTimeOffset LastUpdateOn { get; set; }
    //public int CompanyId { get; set; }
    //public string CompanyName { get; set; }
    //public State State { get; set; }
    public ICollection<ClassifierReferenceCm> Classifiers { get; set; }
    public PurposeReferenceCm Purpose { get; set; }
    public string Notation { get; set; }
    public string Symbol { get; set; }
    public Aspect Aspect { get; set; }
    public ICollection<BlockTerminalLibCm> BlockTerminals { get; set; }
    public ICollection<BlockAttributeLibCm> BlockAttributes { get; set; }
    //public ICollection<SelectedAttributePredefinedLibCm> SelectedAttributePredefined { get; set; }
    public string Kind => nameof(BlockLibCm);
}