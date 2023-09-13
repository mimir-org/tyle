using Mimirorg.Common.Contracts;
using Mimirorg.Common.Enums;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Domain;

namespace Mimirorg.TypeLibrary.Models.Client;

public class AttributeTypeView : ImfTypeView // : IStatefulObject
{
    //public State State { get; set; }
    public PredicateReference? Predicate { get; set; }
    public ICollection<UnitReference> Units { get; set; }
    public int UnitMinCount { get; set; }
    public int UnitMaxCount { get; set; }
    public ProvenanceQualifier? ProvenanceQualifier { get; set; }
    public RangeQualifier? RangeQualifier { get; set; }
    public RegularityQualifier? RegularityQualifier { get; set; }
    public ScopeQualifier? ScopeQualifier { get; set; }
    public ValueConstraintView? ValueConstraint { get; set; }
}