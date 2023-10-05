using Tyle.Core.Attributes;
using Tyle.Core.Common;

namespace Tyle.Api.Attributes;

public class AttributeView : ImfType
{
    public RdlPredicate? Predicate { get; set; }
    public ICollection<RdlUnit> Units { get; set; } = new List<RdlUnit>();
    public int UnitMinCount { get; set; }
    public int UnitMaxCount { get; set; }
    public ProvenanceQualifier? ProvenanceQualifier { get; set; }
    public RangeQualifier? RangeQualifier { get; set; }
    public RegularityQualifier? RegularityQualifier { get; set; }
    public ScopeQualifier? ScopeQualifier { get; set; }
    public ValueConstraintView? ValueConstraint { get; set; }
}