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
}