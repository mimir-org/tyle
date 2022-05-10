using System.ComponentModel.DataAnnotations;
using Mimirorg.TypeLibrary.Extensions;

namespace Mimirorg.TypeLibrary.Models.Application
{
    public class SimpleLibAm
    {
        [Required]
        public string Name { get; set; }

        public ICollection<string> ContentReferences { get; set; }
        public string Description { get; set; }
        public ICollection<string> Attributes { get; set; } // TODO: Bør være objektet AttributeLibAm

        public string Id => $"{Name}".CreateMd5();
    }
}
