using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TypeLibrary.Models.Application
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
    }
}
