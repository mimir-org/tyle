using System.ComponentModel.DataAnnotations;

namespace Tyle.Application.Common.Requests;

[AttributeUsage(AttributeTargets.Property)]
public class ValidIriAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value == null)
        {
            return true; // Use Required if the IRI should not be nullable.
        }

        if (value is string str)
        {
            return Uri.TryCreate(str, UriKind.Absolute, out _);
        }

        return false;
    }
}