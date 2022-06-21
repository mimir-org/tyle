using System.ComponentModel.DataAnnotations;

namespace Mimirorg.TypeLibrary.Models.Application
{
    public class MimirorgUserPermissionAm : IValidatableObject
    {

        [Display(Name = "UserId")]
        [Required(ErrorMessage = "{0} is required")]
        public string UserId { get; set; }

        [Display(Name = "CompanyId")]
        [Required(ErrorMessage = "{0} is required")]
        public int CompanyId { get; set; }

        public ICollection<MimirorgPermissionAm> Permissions { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (Permissions == null)
                return results;

            foreach (var permission in Permissions)
            {
                var v = permission.Validate(validationContext);
                results.AddRange(v);
            }

            return results;
        }
    }
}