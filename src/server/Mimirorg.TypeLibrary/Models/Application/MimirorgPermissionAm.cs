using System.ComponentModel.DataAnnotations;
using Mimirorg.TypeLibrary.Extensions;

namespace Mimirorg.TypeLibrary.Models.Application
{
    public class MimirorgPermissionAm : IValidatableObject, IEqualityComparer<MimirorgPermissionAm>
    {
        [Display(Name = "Id")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(0, 7, ErrorMessage = "The range for {0} is from {1} to {2}")]
        public int Id { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "{0} is required")]
        public string Name { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return this.ValidatePermission();
        }

        public bool Equals(MimirorgPermissionAm x, MimirorgPermissionAm y)
        {
            if (ReferenceEquals(x, y))
                return true;
            if (x == null || y == null)
                return false;
            return x.Id == y.Id && x.Name == y.Name;
        }

        public int GetHashCode(MimirorgPermissionAm obj)
        {
            var hashId = obj.Id.GetHashCode();
            var hashName = obj.Name == null ? 0 : obj.Name.GetHashCode();
            return hashId ^ hashName;
        }
    }
}
