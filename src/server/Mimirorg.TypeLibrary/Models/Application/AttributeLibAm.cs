using System.ComponentModel.DataAnnotations;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Extensions;
using TypeScriptBuilder;
using Discipline = Mimirorg.TypeLibrary.Enums.Discipline;

namespace Mimirorg.TypeLibrary.Models.Application
{
    public class AttributeLibAm : IValidatableObject
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public Aspect Aspect { get; set; }

        [Required]
        public Discipline Discipline { get; set; }

        [Required]
        public Select Select { get; set; }

        [Required]
        public string AttributeQualifier { get; set; }

        [Required]
        public string AttributeSource { get; set; }

        [Required]
        public string AttributeCondition { get; set; }

        [Required]
        public string AttributeFormat { get; set; }

        [Display(Name = "CompanyId")]
        [Range(1, int.MaxValue, ErrorMessage = "{0} must be greater than 0")]
        public int CompanyId { get; set; }

        public ICollection<TypeReferenceAm> TypeReferences { get; set; }
        public ICollection<string> SelectValues { get; set; }
        public ICollection<string> UnitIdList { get; set; }
        public HashSet<string> Tags { get; set; }

        [TSExclude]
        public string Id => ($"{Name}-{Aspect}-{AttributeQualifier}-{AttributeSource}-{AttributeCondition}").CreateMd5();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return this.ValidateAttribute();
        }
    }
}