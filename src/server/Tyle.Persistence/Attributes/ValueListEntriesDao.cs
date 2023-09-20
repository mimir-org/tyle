using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tyle.Persistence.Attributes;

[Table("ValueListEntries")]
public class ValueListEntriesDao
{
    public int Id { get; set; }

    public Guid ValueConstraintId { get; set; }
    public ValueConstraintDao ValueConstraint { get; set; } = null!;

    [Required, MaxLength(500)]
    public required string ValueListEntry { get; set; }
}