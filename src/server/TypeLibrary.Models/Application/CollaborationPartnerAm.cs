using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TypeLibrary.Models.Application
{
    public class CollaborationPartnerAm
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Domain { get; set; }
        public bool Current { get; set; }
        public ICollection<string> Iris { get; set; }
    }
}
