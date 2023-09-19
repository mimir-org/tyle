using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Tyle.Persistence.Common;

[Index(nameof(Iri), IsUnique = true)]
public abstract class ReferenceDao
{
    public int Id { get; }

    [Required, MaxLength(100)]
    public string Name { get; }

    [MaxLength(500)]
    public string? Description { get; }

    [Required, MaxLength(500)]
    public string Iri { get; }

    [Required, MaxLength(50)]
    public string Source { get; }
}
