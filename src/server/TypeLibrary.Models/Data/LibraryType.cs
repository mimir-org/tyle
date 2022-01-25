using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TypeLibrary.Models.Enums;
using TypeLibrary.Models.Extensions;

namespace TypeLibrary.Models.Data
{
    public class LibraryType
    {
        public string Id { get; set; }
        public string Version { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [Required]
        public string StatusId { get; set; }

        public string TypeId { get; set; }
        public string SemanticReference { get; set; }
        public Aspect Aspect { get; set; }

        public string RdsId { get; set; }
        public Rds Rds { get; set; }
        public string PurposeId { get; set; }
        public Purpose Purpose { get; set; }

        public string UpdatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }

        public ICollection<Collection> Collections { get; set; }

        public void IncrementMinorVersion()
        {
            Version = Version.IncrementMinorVersion();
        }

        public void IncrementMajorVersion()
        {
            Version = Version.IncrementMajorVersion();
        }
    }
}
