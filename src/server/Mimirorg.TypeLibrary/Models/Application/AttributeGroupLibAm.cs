using System.ComponentModel.DataAnnotations;
using Mimirorg.TypeLibrary.Models.Client;

namespace Mimirorg.TypeLibrary.Models.Application
{
    public class AttributeGroupLibAm
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<string> AttributeIds { get; set; }
    }
}