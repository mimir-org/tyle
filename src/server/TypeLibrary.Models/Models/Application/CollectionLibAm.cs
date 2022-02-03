using System;
using System.ComponentModel.DataAnnotations;

namespace TypeLibrary.Models.Models.Application
{
    public class CollectionLibAm
    {
        public string Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
        public int? CompanyId { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? Updated { get; set; }
    }
}