using Mimirorg.Common.Enums;
using Mimirorg.TypeLibrary.Enums;

namespace Mimirorg.TypeLibrary.Models.Client;

public class TerminalLibCm
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTimeOffset CreatedOn { get; set; }
    public string CreatedBy { get; set; }
    public ICollection<string> ContributedBy { get; set; }
    public DateTimeOffset LastUpdateOn { get; set; }
    //public State State { get; set; }
    public ICollection<string> Classifiers { get; set; }
    public string Purpose { get; set; }
    public string Notation { get; set; }
    public string Symbol { get; set; }
    public Aspect Aspect { get; set; }
    public string Medium { get; set; }
    public Direction Qualifier { get; set; }
    public ICollection<TerminalAttributeLibCm> TerminalAttributes { get; set; }
    public string Kind => nameof(TerminalLibCm);
}