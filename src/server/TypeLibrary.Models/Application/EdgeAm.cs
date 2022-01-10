using System;
using System.ComponentModel.DataAnnotations;
using TypeLibrary.Models.Extensions;

namespace TypeLibrary.Models.Application
{
    public class EdgeAm
    {
        public string Id { get; set; }
        public string Iri { get; set; }
        public string Domain => Id.ResolveDomain();
        [Required]
        public string ProjectId { get; set; }
        [Required]
        public string FromConnectorId { get; set; }
        public string FromConnectorIri { get; set; }
        [Required]
        public string ToConnectorId { get; set; }
        public string ToConnectorIri { get; set; }
        [Required]
        public string FromNodeId { get; set; }
        public string FromNodeIri { get; set; }
        [Required]
        public string ToNodeId { get; set; }
        public string ToNodeIri { get; set; }
        [Required]
        public string MasterProjectId { get; set; }
        public string MasterProjectIri { get; set; }
        public bool IsLocked { get; set; }
        public string IsLockedStatusBy { get; set; }
        public DateTime? IsLockedStatusDate { get; set; }
        public TransportAm Transport { get; set; }
        public InterfaceAm Interface { get; set; }
    }
}