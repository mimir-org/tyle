using System.ComponentModel.DataAnnotations;

namespace Mimirorg.Authentication.Models.Application
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
            if (Permissions == null) 
                yield break;
            
            foreach (var permission in Permissions)
            {
                var v = permission.Validate(validationContext);
                foreach (var result in v)
                {
                    yield return result;
                }
            }
        }
    }
}
