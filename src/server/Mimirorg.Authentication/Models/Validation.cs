using System.ComponentModel.DataAnnotations;

namespace Mimirorg.Authentication.Models;

public class Validation
{
    public string Message { get; set; }
    public bool IsValid { get; set; }
    public List<ValidationResult> Result { get; set; }

    public Validation()
    {
        Result = new List<ValidationResult>();
    }

    public Validation(string key, string message, bool isValid = false)
    {
        Result = new List<ValidationResult>();
        Message = message;
        IsValid = isValid;
        Result = new List<ValidationResult>
        {
            new(message, new List<string> {key})
        };
    }

    public Validation(IEnumerable<string> keys, string message, bool isValid = false)
    {
        Result = new List<ValidationResult>();
        Message = message;
        IsValid = isValid;
        Result = new List<ValidationResult>
        {
            new(message, keys)
        };
    }

    public void AddNotAllowToChange(string key)
    {
        Result.Add(new ValidationResult("The property is not allowed to be changed.", new List<string> { key }));
    }

    public void AddNotAllowToChange(string key, string message)
    {
        Result.Add(new ValidationResult(message, new List<string> { key }));
    }
}