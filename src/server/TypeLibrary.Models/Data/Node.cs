using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using TypeLibrary.Models.Data.Enums;
using TypeLibrary.Models.Enums;
using TypeLibrary.Models.Extensions;

namespace TypeLibrary.Models.Data
{
    [Serializable]
    public class Node
    {
        public string Id { get; set; }
        public string Iri { get; set; }

        public string Domain => Id.ResolveDomain();
        
        public string Kind => nameof(Node);

        public string Rds { get; set; }

        public string Description { get; set; }

        public string SemanticReference { get; set; }

        [Required]
        public string Name { get; set; }

        public string Label { get; set; }

        [Required]
        public decimal PositionX { get; set; }

        [Required]
        public decimal PositionY { get; set; }

        public bool IsLocked { get; set; }
        public string IsLockedStatusBy { get; set; }
        public DateTime? IsLockedStatusDate { get; set; }

        [Required]
        public decimal PositionBlockX { get; set; }

        [Required]
        public decimal PositionBlockY { get; set; }

        [Required]
        public int Level { get; set; }

        [Required]
        public int Order { get; set; }

        [Required]
        public string StatusId { get; set; }

        //public BuildStatus Status { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime Updated { get; set; }

        public DateTime? Created { get; set; }

        public string CreatedBy { get; set; }

        public string LibraryTypeId { get; set; }

        public string Version { get; set; }

        public Aspect Aspect { get; set; }

        [Required]
        public bool IsRoot { get; set; }

        [Required]
        public string MasterProjectId { get; set; }

        [Required]
        public string MasterProjectIri { get; set; }

        public string Symbol { get; set; }

        public string PurposeString { get; set; }

        [NotMapped]
        public virtual Purpose Purpose { get; set; }

        public virtual ICollection<Connector> Connectors { get; set; }

        public virtual ICollection<Attribute> Attributes { get; set; }

        public virtual ICollection<Simple> Simples { get; set; }

        [Required]
        public virtual string ProjectId { get; set; }

        [JsonIgnore]
        public virtual Project Project { get; set; }

        [JsonIgnore]
        public virtual ICollection<Edge> FromEdges { get; set; }

        [JsonIgnore]
        public virtual ICollection<Edge> ToEdges { get; set; }

        // Required Only for location aspect
        public decimal? Length { get; set; }

        public decimal? Width { get; set; }

        public decimal? Height { get; set; }

        public decimal? Area => Length * Width;

        // Required only for product aspect
        public decimal? Cost { get; set; }

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
