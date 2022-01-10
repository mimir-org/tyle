using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TypeLibrary.Models.Extensions;

namespace TypeLibrary.Models.Data
{
    [Serializable]
    public class Project
    {
        #region Properties

        public string Id { get; set; }
        public string Iri { get; set; }

        public string Domain => Id.ResolveDomain();
        
        [Required]
        public bool IsSubProject { get; set; }

        [Required]
        public string Version { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public string ProjectOwner { get; set; }

        [Required]
        public string UpdatedBy { get; set; }

        [Required]
        public DateTime Updated { get; set; }

        public virtual ICollection<Node> Nodes { get; set; }
        public virtual ICollection<Edge> Edges { get; set; }

        #endregion

        #region Public methods

        public void IncrementMajorVersion()
        {
            if (Version.Length == 3)
                Version += ".0";

            Version = Version.IncrementMajorVersion();
        }

        public void IncrementMinorVersion()
        {
            if (Version.Length == 3)
                Version += ".0";

            Version = Version.IncrementMinorVersion();
        }

        public void IncrementCommitVersion()
        {
            if (Version.Length == 3)
                Version += ".0";

            Version = Version.IncrementCommitVersion();
        }

        #endregion
    }
}
