using Tyle.Core.Common;

namespace Tyle.Core.Attributes;

public class AttributeType : ImfType
{
    public PredicateReference? Predicate { get; }
    public ICollection<UnitReference> Units { get; }
    public int UnitMinCount { get; }
    public int UnitMaxCount { get; }
    public ProvenanceQualifier? ProvenanceQualifier { get; }
    public RangeQualifier? RangeQualifier { get; }
    public RegularityQualifier? RegularityQualifier { get; }
    public ScopeQualifier? ScopeQualifier { get; }
    public ValueConstraint? ValueConstraint { get; }

    public AttributeType(string name, string? description, User createdBy) : base(name, description, createdBy)
    {
        Units = new HashSet<UnitReference>();
    }
}