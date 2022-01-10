using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using TypeLibrary.Models.Extensions;

namespace TypeLibrary.Models.Data
{
    [Serializable]
    public class Edge
    {
        public string Id { get; set; }
        public string Iri { get; set; }

        public string Domain => Id.ResolveDomain();
        
        public string Kind => nameof(Edge);

        public string FromConnectorId { get; set; }
        public string FromConnectorIri { get; set; }
        public Connector FromConnector { get; set; }
        
        public string ToConnectorId { get; set; }
        public string ToConnectorIri { get; set; }
        public Connector ToConnector { get; set; }
        
        public string FromNodeId { get; set; }
        public string FromNodeIri { get; set; }
        public Node FromNode { get; set; }
        
        public string ToNodeId { get; set; }
        public string ToNodeIri { get; set; }
        public Node ToNode { get; set; }

        public string TransportId { get; set; }
        public Transport Transport { get; set; }

        public string InterfaceId { get; set; }
        public Interface Interface { get; set; }

        public bool IsLocked { get; set; }
        public string IsLockedStatusBy { get; set; }
        public DateTime? IsLockedStatusDate { get; set; }

        [Required]
        public string MasterProjectId { get; set; }
        public string MasterProjectIri { get; set; }

        [Required]
        public virtual string ProjectId { get; set; }

        [JsonIgnore]
        public virtual Project Project { get; set; }
        
        
    }
}
