using Tyle.Core.Common;

namespace Tyle.Core.Attributes;

public class AttributeType : ImfType
{
    public int? PredicateId { get; set; }
    public PredicateReference? Predicate { get; set; }
    public ICollection<AttributeUnitMapping> Units { get; }
    public int UnitMinCount { get; set; }
    public int UnitMaxCount { get; set; }
    public ProvenanceQualifier? ProvenanceQualifier { get; set; }
    public RangeQualifier? RangeQualifier { get; set; }
    public RegularityQualifier? RegularityQualifier { get; set; }
    public ScopeQualifier? ScopeQualifier { get; set; }
    public ValueConstraint? ValueConstraint { get; set; }

    public AttributeType(string name, string? description, string createdBy) : base(name, description, createdBy)
    {
        Units = new List<AttributeUnitMapping>();
    }
}