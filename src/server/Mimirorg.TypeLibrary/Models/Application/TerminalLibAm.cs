using System.ComponentModel.DataAnnotations;
using Mimirorg.TypeLibrary.Extensions;

namespace Mimirorg.TypeLibrary.Models.Application
{
    public class TerminalLibAm
    {
        [Required]
        public string Name { get; set; }

        public string ParentId { get; set; }
        public ICollection<string> ContentReferences { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public ICollection<string> AttributeIdList { get; set; }

        public string Id => $"{Name}".CreateMd5();
    }
}