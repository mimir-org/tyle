using System.ComponentModel.DataAnnotations;

namespace Mimirorg.TypeLibrary.Models.Application
{
    public class AttributeGroupLibAm
    {
        [Required]
        public string Name { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public List<string> Attributes { get; set; }
    }
}