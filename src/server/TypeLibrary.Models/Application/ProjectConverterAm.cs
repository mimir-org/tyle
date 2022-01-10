using System;
using System.ComponentModel.DataAnnotations;

namespace TypeLibrary.Models.Application
{
    public class ProjectConverterAm
    {
        [Required]
        public Guid ParserId { get; set; }

        [Required]
        public ProjectAm Project { get; set; }
    }
}
