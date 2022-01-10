using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using TypeLibrary.Models.Data.Enums;
using TypeLibrary.Models.Extensions;

namespace TypeLibrary.Models.Data
{
    public class Interface
    {
        public string Id { get; set; }
        public string Iri { get; set; }
        public string Version { get; set; }
        public string Rds { get; set; }
        
        public string Kind => nameof(Interface);

        [Required]
        public string Name { get; set; }

        public string Label { get; set; }
        public string Description { get; set; }

        [Required]
        public string StatusId { get; set; }

        public string SemanticReference { get; set; }
        public ICollection<Attribute> Attributes { get; set; }
        public string InputTerminalId { get; set; }
        public virtual Terminal InputTerminal { get; set; }
        public string OutputTerminalId { get; set; }
        public virtual Terminal OutputTerminal { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? Created { get; set; }
        public string CreatedBy { get; set; }
        public string LibraryTypeId { get; set; }
        //public BuildStatus Status { get; set; }

        [JsonIgnore]
        public ICollection<Edge> Edges { get; set; }

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
