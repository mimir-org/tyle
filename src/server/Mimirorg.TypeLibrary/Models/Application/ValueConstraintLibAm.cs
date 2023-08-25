using System.ComponentModel.DataAnnotations;
using Mimirorg.TypeLibrary.Enums;

namespace Mimirorg.TypeLibrary.Models.Application;

public class ValueConstraintLibAm
{
    [Required]
    public string AttributeId { get; set; }

    [Required]
    public ConstraintType ConstraintType { get; set; }

    public string Value { get; set; }
    
    public ICollection<string> AllowedValues { get; set; }

    [Required]
    public XsdDataType DataType { get; set; }

    public int? MinCount { get; set; }
    public int? MaxCount { get; set; }

    public string Pattern { get; set; }
    
    public decimal? MinValue { get; set; }
    public decimal? MaxValue { get; set; }
    public bool? MinInclusive { get; set; }
    public bool? MaxInclusive { get; set; }
}