using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tyle.Persistence.Common;

namespace Tyle.Persistence.Terminals;

[Table("Terminal")]
public class TerminalDao
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

    public ICollection<TerminalClassifierDao> TerminalClassifiers { get; set; } = new List<TerminalClassifierDao>();

    public int? PurposeId { get; set; }
    public PurposeDao? Purpose { get; set; }

    [MaxLength(50)]
    public string? Notation { get; set; }

    [MaxLength(500)]
    public string? Symbol { get; set; }

    [MaxLength(20)]
    public string? Aspect { get; set; }

    public int? MediumId { get; set; }
    public MediumDao? Medium { get; set; }

    [Required, MaxLength(20)]
    public required string Qualifier { get; set; }

    public ICollection<TerminalAttributeDao> TerminalAttributes { get; set; } = new List<TerminalAttributeDao>();
}
