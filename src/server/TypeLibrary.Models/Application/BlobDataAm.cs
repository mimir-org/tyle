using System.ComponentModel.DataAnnotations;
using TypeLibrary.Models.Enums;

namespace TypeLibrary.Models.Application
{
    public class BlobDataAm
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
