using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tyle.Persistence.Terminals;

[Table("Medium")]
[Index(nameof(Iri), IsUnique = true)]
public class MediumDao
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public required string Name { get; set; }

    [MaxLength(500)]
    public string? Description { get; set; }

    [Required, MaxLength(500)]
    public required string Iri { get; set; }

    [Required, MaxLength(50)]
    public required string Source { get; set; }
}
