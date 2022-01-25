using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TypeLibrary.Models.Enums;

namespace TypeLibrary.Models.Application
{
    public class LocationAm
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Iri { get; set; }
        public string ParentId { get; set; }
        public LocationAm Parent { get; set; }
        public ICollection<LocationAm> Children { get; set; }
        public Aspect Aspect { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
    }
}