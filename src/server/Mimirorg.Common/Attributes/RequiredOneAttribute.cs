using System.ComponentModel.DataAnnotations;

namespace Mimirorg.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RequiredOneAttribute : ValidationAttribute
    {
        private readonly string _dependent;

        public RequiredOneAttribute(string dependent)
        {
            _dependent = dependent;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dependentValue = validationContext?.ObjectInstance.GetType().GetProperty(_dependent)?.GetValue(validationContext.ObjectInstance, null);

            if (string.IsNullOrWhiteSpace(value?.ToString()) && string.IsNullOrWhiteSpace(dependentValue?.ToString()))
                return new ValidationResult("One of those fields are required.", new List<string> { validationContext?.MemberName, _dependent });

            return ValidationResult.Success;
        }
    }
}