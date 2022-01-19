using System.ComponentModel.DataAnnotations;

namespace TypeLibrary.Models.Application
{
    public class UnitAm
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual string Description { get; set; }
        public string Iri { get; set; }
    }
}