﻿using System.ComponentModel.DataAnnotations;

namespace Mimirorg.TypeLibrary.Models.Application
{
    public class AttributeConditionLibAm
    {
        [Required]
        public string Name { get; set; }
        
        public ICollection<string> ContentReferences { get; set; }
        public string Description { get; set; }
        
        public string UpdatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
    }
}