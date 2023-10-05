using TypeLibrary.Core.Common;

namespace TypeLibrary.Core.Attributes
{
    public class AttributeType : ImfType
    {
        public int? PredicateId { get; set; }
        public RdlPredicate? Predicate { get; set; }
        public ICollection<AttributeUnitJoin> Units { get; set; } = new List<AttributeUnitJoin>();
        public int UnitMinCount { get; set; }
        public int UnitMaxCount { get; set; }
        public ProvenanceQualifier? ProvenanceQualifier { get; set; }
        public RangeQualifier? RangeQualifier { get; set; }
        public RegularityQualifier? RegularityQualifier { get; set; }
        public ScopeQualifier? ScopeQualifier { get; set; }
        public ValueConstraint? ValueConstraint { get; set; }
    }
}