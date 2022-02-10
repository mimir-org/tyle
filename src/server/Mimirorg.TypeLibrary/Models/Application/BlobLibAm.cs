using System.ComponentModel.DataAnnotations;
using Mimirorg.Common.Enums;

namespace Mimirorg.TypeLibrary.Models.Application
{
    public class BlobLibAm
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        public string Iri { get; set; }

        [Required]
        public string Data { get; set; }

        [Required]
        public Discipline Discipline { get; set; }
    }
}
