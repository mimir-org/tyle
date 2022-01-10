#nullable enable
using System.ComponentModel.DataAnnotations;
using TypeLibrary.Models.Application;

namespace TypeLibrary.Models.Attributes
{
    public class ValidatePositiveDecimalAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (!(validationContext.ObjectInstance is NodeAm nodeAm))
                return new ValidationResult("This validation attribute could only be used on a NodeAm object");

            if (value == null)
                return ValidationResult.Success;

            if (value is >= 0.0m)
                return ValidationResult.Success;

            var memberName = validationContext.MemberName ?? "";
            var result = new ValidationResult($"Property {memberName} must be a positive decimal");

            return result;
        }
    }
}
