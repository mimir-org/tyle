using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TypeLibrary.Models.Application
{
    public class SimpleAm
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string SemanticReference { get; set; }
        public virtual ICollection<AttributeAm> Attributes { get; set; }
        [Required]
        public virtual string NodeId { get; set; }
    }
}
