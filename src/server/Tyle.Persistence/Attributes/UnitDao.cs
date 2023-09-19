using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tyle.Persistence.Common;

namespace Tyle.Persistence.Attributes;

[Table("Units")]
public class UnitDao : ReferenceDao
{
    [MaxLength(30)]
    public string? Symbol { get; }
}
