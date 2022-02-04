using System.ComponentModel.DataAnnotations;
using Mimirorg.Common.Enums;

namespace Mimirorg.TypeLibrary.Models.Application
{
    public class BlobDataLibAm
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Data { get; set; }

        [Required]
        public Discipline Discipline { get; set; }
    }
}
