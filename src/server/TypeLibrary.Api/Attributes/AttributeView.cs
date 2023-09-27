using TypeLibrary.Core.Attributes;
using TypeLibrary.Core.Common;

namespace TypeLibrary.Api.Attributes;

public class AttributeView : ImfType
{
    public RdlPredicate? Predicate { get; set; }
    public ICollection<RdlUnit> Units { get; set; }
    public int UnitMinCount { get; set; }
    public int UnitMaxCount { get; set; }
    public ProvenanceQualifier? ProvenanceQualifier { get; set; }
    public RangeQualifier? RangeQualifier { get; set; }
    public RegularityQualifier? RegularityQualifier { get; set; }
    public ScopeQualifier? ScopeQualifier { get; set; }
    public ValueConstraintView? ValueConstraint { get; set; }
}