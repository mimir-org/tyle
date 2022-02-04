using System.ComponentModel.DataAnnotations;
using Mimirorg.Common.Enums;

namespace TypeLibrary.Models.Models.Application
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
