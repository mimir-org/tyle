using System.ComponentModel.DataAnnotations;

namespace Mimirorg.Authentication.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class DigitAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not string s)
            return new ValidationResult("This validation attribute could only be used on a string object");

        var list = s.ToCharArray();

        return list.Any(x => !char.IsDigit(x)) ?
            new ValidationResult($"Property {validationContext.MemberName} must only contain digits") :
            ValidationResult.Success;
    }
}