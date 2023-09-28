using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Options;
using Mimirorg.Authentication.Extensions;
using Mimirorg.Authentication.Models;

namespace Mimirorg.Authentication.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class PasswordAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var authSettings = ((IOptions<MimirorgAuthSettings>) validationContext.GetService(typeof(IOptions<MimirorgAuthSettings>)))?.Value;

        return value is not string s ?
            new ValidationResult("The password must be a string.", new List<string> { "Password", "ConfirmPassword" }) :
            s.HasValidPassword(authSettings);
    }
}