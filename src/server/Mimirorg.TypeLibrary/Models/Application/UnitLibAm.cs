using System.ComponentModel.DataAnnotations;
using Mimirorg.TypeLibrary.Extensions;

namespace Mimirorg.TypeLibrary.Models.Application
{
    public class UnitLibAm
    {
        [Required]
        public string Name { get; set; }
        public ICollection<string> ContentReferences { get; set; }
        public virtual string Description { get; set; }
        
        private const string InternalType = "Mb.Models.Data.Enums.Unit";

        public virtual string Id => $"{Name}-{InternalType}".CreateMd5();
    }
}