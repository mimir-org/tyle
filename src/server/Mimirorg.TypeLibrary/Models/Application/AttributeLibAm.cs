using System.ComponentModel.DataAnnotations;
using Mimirorg.TypeLibrary.Enums;

namespace Mimirorg.TypeLibrary.Models.Application;

public class AttributeLibAm
{
    [Required]
    public string Name { get; set; }

    public string Description { get; set; }

    public string Predicate { get; set; }

    [Required]
    public ICollection<string> UoMs { get; set; }

    public ProvenanceQualifier ProvenanceQualifier { get; set; }
    public RangeQualifier RangeQualifier { get; set; }
    public RegularityQualifier RegularityQualifier { get; set; }
    public ScopeQualifier ScopeQualifier { get; set; }

    public ValueConstraintLibAm ValueConstraint { get; set; }
}