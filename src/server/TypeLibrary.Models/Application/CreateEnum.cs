using System.ComponentModel.DataAnnotations;
using TypeLibrary.Models.Enums;

namespace TypeLibrary.Models.Application
{
    public class CreateEnum
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public EnumType EnumType { get; set; }
        
        public string Description { get; set; }
        
        public string SemanticReference { get; set; }
    }
}
