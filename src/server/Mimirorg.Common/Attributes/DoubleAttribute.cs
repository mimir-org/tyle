using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Mimirorg.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DoubleAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is not string)
                return new ValidationResult("This validation attribute could only be used on a string object");

            var v = (string) value;
            if (v.All(x => x != '.'))
                return new ValidationResult($"Property {validationContext.MemberName} must be in format x.y");

            var split = v.Split('.', StringSplitOptions.RemoveEmptyEntries);
            if (split.Length != 2)
                return new ValidationResult($"Property {validationContext.MemberName} must be in format x.y");

            if (!double.TryParse(v, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out _))
                return new ValidationResult($"Property {validationContext.MemberName} must be in format x.y");

            return ValidationResult.Success;
        }
    }
}