using System.ComponentModel.DataAnnotations;

namespace Mimirorg.Common.Models
{
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
                new ValidationResult(message, new List<string> {key})
            };
        }
    }
}