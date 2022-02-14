using System.ComponentModel.DataAnnotations;
using Mimirorg.TypeLibrary.Extensions;
using Discipline = Mimirorg.TypeLibrary.Enums.Discipline;

namespace Mimirorg.TypeLibrary.Models.Application
{
    public class BlobLibAm
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public Discipline Discipline { get; set; }

        [Required]
        public string Data { get; set; }

        public virtual string Key => $"{Name}-{Discipline}".CreateMd5();
    }
}
