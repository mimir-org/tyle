using System.ComponentModel.DataAnnotations;
using Mimirorg.TypeLibrary.Extensions;
using TypeScriptBuilder;

namespace Mimirorg.TypeLibrary.Models.Application
{
    public class PurposeLibAm
    {
        [Required]
        public string Name { get; set; }

        public ICollection<string> ContentReferences { get; set; }
        public string Description { get; set; }

        [TSExclude]
        public string Id => $"{Name}".CreateMd5();
    }
}