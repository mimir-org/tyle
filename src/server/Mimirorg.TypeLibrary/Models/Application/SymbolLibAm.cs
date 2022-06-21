using System.ComponentModel.DataAnnotations;
using Mimirorg.TypeLibrary.Extensions;
using TypeScriptBuilder;

namespace Mimirorg.TypeLibrary.Models.Application
{
    public class SymbolLibAm
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Data { get; set; }

        public ICollection<string> ContentReferences { get; set; }

        [TSExclude]
        public virtual string Id => $"{Name}".CreateMd5();
    }
}