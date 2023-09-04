using Mimirorg.Common.Contracts;
using Mimirorg.Common.Enums;
using Mimirorg.TypeLibrary.Enums;

namespace Mimirorg.TypeLibrary.Models.Client;

public class AttributeLibCm // : IStatefulObject
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTimeOffset CreatedOn { get; set; }
    public string CreatedBy { get; set; }
    public ICollection<string> ContributedBy { get; set; }
    public DateTimeOffset LastUpdateOn { get; set; }
    //public State State { get; set; }
    public string Predicate { get; set; }
    public ICollection<string> UoMs { get; set; }
    public ProvenanceQualifier? ProvenanceQualifier { get; set; }
    public RangeQualifier? RangeQualifier { get; set; }
    public RegularityQualifier? RegularityQualifier { get; set; }
    public ScopeQualifier? ScopeQualifier { get; set; }
    public ValueConstraintLibCm ValueConstraint { get; set; }

    public string Kind => nameof(AttributeLibCm);
}