using System;
using System.ComponentModel.DataAnnotations;

namespace TypeLibrary.Models.Application
{
    public class ProjectFileAm
    {
        [Required]
        public Guid ParserId { get; set; }

        [Required]
        public string FileContent { get; set; }

        public FileFormat FileFormat { get; set; }
    }
}
