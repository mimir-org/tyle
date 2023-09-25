using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tyle.Persistence.Attributes;

[Table("Unit")]
[Index(nameof(Iri), IsUnique = true)]
public class UnitDao
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public required string Name { get; set; }

    [MaxLength(30)]
    public string? Symbol { get; set; }

    [MaxLength(500)]
    public string? Description { get; set; }

    [Required, MaxLength(500)]
    public required string Iri { get; set; }

    [Required, MaxLength(50)]
    public required string Source { get; set; }
}
