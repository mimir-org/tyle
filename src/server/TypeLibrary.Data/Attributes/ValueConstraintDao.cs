using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Tyle.Persistence.Attributes;

[Table("ValueConstraint")]
public class ValueConstraintDao
{
    [Key]
    public Guid AttributeId { get; set; }

    public AttributeDao Attribute { get; set; } = null!;

    [Required, MaxLength(20)]
    public required string ConstraintType { get; set; }

    [Required, MaxLength(20)]
    public required string DataType { get; set; }

    public int? MinCount { get; set; }
    public int? MaxCount { get; set; }

    [MaxLength(500)]
    public string? Value { get; set; }

    public ICollection<ValueListEntryDao> ValueList { get; set; } = new List<ValueListEntryDao>();

    [MaxLength(500)]
    public string? Pattern { get; set; }

    [Precision(38, 19)]
    public decimal? MinValue { get; set; }

    [Precision(38, 19)]
    public decimal? MaxValue { get; set; }

    public bool? MinInclusive { get; set; }

    public bool? MaxInclusive { get; set; }
}