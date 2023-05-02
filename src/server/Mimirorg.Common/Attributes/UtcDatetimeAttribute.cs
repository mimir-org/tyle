using System.ComponentModel.DataAnnotations;
using DateTime = System.DateTime;

namespace Mimirorg.Common.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class UtcDatetimeAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is not DateTime dt)
            return new ValidationResult("This validation attribute could only be used on a DateTime object");

        if (dt.Kind != DateTimeKind.Utc)
            return new ValidationResult($"Property {validationContext.MemberName} must be in UTC format (ISO8601)");

        return ValidationResult.Success;
    }
}