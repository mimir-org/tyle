using System.ComponentModel.DataAnnotations;

namespace Mimirorg.TypeLibrary.Models.Application
{
    public class RdsLibAm
    {
        [Required]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<string> ContentReferences { get; set; }
    }
}