using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tyle.Persistence.Attributes;

[Table("Attribute")]
public class AttributeDao
{
    public Guid Id { get; set; }

    [Required, MaxLength(100)]
    public required string Name { get; set; }

    [MaxLength(500)]
    public string? Description { get; set; }

    [Required, MaxLength(10)]
    public required string Version { get; set; }

    public DateTimeOffset CreatedOn { get; set; }

    // TODO: Implement created by and contributed by

    public DateTimeOffset LastUpdateOn { get; set; }

    public int? PredicateId { get; set; }
    public PredicateDao? Predicate { get; set; }

    public ICollection<AttributeUnitDao> AttributeUnits { get; set; } = new List<AttributeUnitDao>();
    public int UnitMinCount { get; set; }
    public int UnitMaxCount { get; set; }

    [MaxLength(20)]
    public string? ProvenanceQualifier { get; set; }

    [MaxLength(20)]
    public string? RangeQualifier { get; set; }

    [MaxLength(20)]
    public string? RegularityQualifier { get; set; }

    [MaxLength(20)]
    public string? ScopeQualifier { get; set; }

    public ValueConstraintDao? ValueConstraint { get; set; }
}
