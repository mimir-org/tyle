using Tyle.Core.Attributes.ValueConstraints;
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
    public IValueConstraint? ValueConstraint { get; }

    /// <summary>
    /// Creates a new attribute type.
    /// </summary>
    /// <param name="name">The name of the attribute type.</param>
    /// <param name="description">A description of the attribute type. Can be null.</param>
    /// <param name="createdBy">A user struct containing information about the user creating the type.</param>
    public AttributeType(string name, string? description, User createdBy) : base(name, description, createdBy)
    {
        Units = new List<UnitReference>();
    }
}