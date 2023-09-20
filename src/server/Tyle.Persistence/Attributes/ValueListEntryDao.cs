using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tyle.Persistence.Attributes;

[Table("ValueListEntry")]
public class ValueListEntryDao
{
    public int Id { get; set; }

    public Guid ValueConstraintId { get; set; }
    public ValueConstraintDao ValueConstraint { get; set; } = null!;

    [Required, MaxLength(500)]
    public string ValueListEntry { get; set; }

    public ValueListEntryDao(Guid valueConstraintId, string valueListEntry)
    {
        ValueConstraintId = valueConstraintId;
        ValueListEntry = valueListEntry;
    }
}