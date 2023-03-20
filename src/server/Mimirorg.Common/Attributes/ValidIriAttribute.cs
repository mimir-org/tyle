using System.ComponentModel.DataAnnotations;

namespace Mimirorg.Common.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class ValidIriAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return ValidationResult.Success;

            if (Uri.TryCreate(str, UriKind.Absolute, out var uri))
            {
                var segments = uri.Segments;
                if (uri.Segments.Length >= 2)
                    return ValidationResult.Success;
            }

            return new ValidationResult("The uri is not valid. Uri must be absolute, and contain minimum one segment.", new List<string> { validationContext?.MemberName });

        }

        return ValidationResult.Success;
    }
}