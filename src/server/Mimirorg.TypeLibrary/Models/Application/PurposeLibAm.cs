using System.ComponentModel.DataAnnotations;
using Mimirorg.TypeLibrary.Extensions;

namespace Mimirorg.TypeLibrary.Models.Application
{
    public class PurposeLibAm
    {
        [Required]
        public string Name { get; set; }
        public ICollection<string> ContentReferences { get; set; }
        public string Description { get; set; }

        public virtual string Id => $"{Name}".CreateMd5();
    }
}