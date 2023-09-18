using Tyle.Core.Attributes.ValueConstraints;
using Tyle.Core.Common;

namespace Tyle.Core.Attributes;

public class AttributeType : ImfType
{
    public PredicateReference? Predicate { get; set; }
    public ICollection<UnitReference> Units { get; }
    public int UnitMinCount { get; set; }
    public int UnitMaxCount { get; set; }
    public ProvenanceQualifier? ProvenanceQualifier { get; set; }
    public RangeQualifier? RangeQualifier { get; set; }
    public RegularityQualifier? RegularityQualifier { get; set; }
    public ScopeQualifier? ScopeQualifier { get; set; }
    public IValueConstraint? ValueConstraint { get; set; }

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