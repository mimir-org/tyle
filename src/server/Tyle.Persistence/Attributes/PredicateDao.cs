using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tyle.Persistence.Attributes;

[Table("Predicates")]
[Index(nameof(Iri), IsUnique = true)]
public class PredicateDao
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; }

    [MaxLength(500)]
    public string? Description { get; set; }

    [Required, MaxLength(500)]
    public string Iri { get; set; }

    [Required, MaxLength(50)]
    public string Source { get; set; }
}
