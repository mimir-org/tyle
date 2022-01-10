using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TypeLibrary.Models.Enums;
using TypeLibrary.Models.Extensions;

namespace TypeLibrary.Models.Data
{
    [Serializable]
    public class Connector
    {
        #region Properties

        public string Id { get; set; }
        public string Iri { get; set; }
        public string Domain => Id.ResolveDomain();
        
        public string Kind => nameof(Connector);
        public string Name { get; set; }
        public ConnectorType Type { get; set; }
        public string SemanticReference { get; set; }
        public bool Visible { get; set; }
        public virtual string NodeId { get; set; }
        public virtual string NodeIri { get; set; }
        public bool IsRequired { get; set; }

        [JsonIgnore]
        public virtual Node Node { get; set; }

        [JsonIgnore]
        public virtual ICollection<Edge> FromEdges { get; set; }

        [JsonIgnore]
        public virtual ICollection<Edge> ToEdges { get; set; }

        #endregion
    }
}
