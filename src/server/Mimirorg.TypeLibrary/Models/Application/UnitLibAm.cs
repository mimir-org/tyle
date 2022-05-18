using System.ComponentModel.DataAnnotations;
using Mimirorg.TypeLibrary.Extensions;
using TypeScriptBuilder;

namespace Mimirorg.TypeLibrary.Models.Application
{
    public class UnitLibAm
    {
        [Required]
        public string Name { get; set; }
        public ICollection<string> ContentReferences { get; set; }
        public virtual string Description { get; set; }

        [TSExclude]
        private const string InternalType = "Mb.Models.Data.Enums.Unit";

        [TSExclude]
        public virtual string Id => $"{Name}-{InternalType}".CreateMd5();
    }
}