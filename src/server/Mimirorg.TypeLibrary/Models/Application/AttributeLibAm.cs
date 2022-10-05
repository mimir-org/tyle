using System.ComponentModel.DataAnnotations;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Extensions;
using TypeScriptBuilder;
using Discipline = Mimirorg.TypeLibrary.Enums.Discipline;
// ReSharper disable InconsistentNaming

namespace Mimirorg.TypeLibrary.Models.Application
{
    public class AttributeLibAm : IValidatableObject
    {
        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Aspect")]
        [Required]
        public Aspect Aspect { get; set; }

        [Display(Name = "Discipline")]
        [Required]
        public Discipline Discipline { get; set; }

        [Display(Name = "Select")]
        [Required]
        public Select Select { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Quantity Datum with specified Scope")]
        public string QuantityDatumSpecifiedScope { get; set; }

        [Display(Name = "Quantity Datum with specified Provenance")]
        public string QuantityDatumSpecifiedProvenance { get; set; }

        [Display(Name = "Range Specifying Quantity Datum")]
        public string QuantityDatumRangeSpecifying { get; set; }

        [Display(Name = "Regularity Specified Quantity Datum")]
        public string QuantityDatumRegularitySpecified { get; set; }

        [Display(Name = "CompanyId")]
        [Range(1, int.MaxValue, ErrorMessage = "{0} must be greater than 0")]
        public int CompanyId { get; set; }

        public ICollection<TypeReferenceAm> TypeReferences { get; set; }
        public ICollection<string> SelectValues { get; set; }
        public ICollection<string> UnitIdList { get; set; }

        [TSExclude]
        public string Version { get; set; } = "1.0";

        [TSExclude]
        public string Id => ($"{Name}-{Version}-{Aspect}-{QuantityDatumSpecifiedScope}{QuantityDatumSpecifiedProvenance}{QuantityDatumRangeSpecifying}{QuantityDatumRegularitySpecified}").CreateMd5();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return this.ValidateAttribute();
        }
    }
}