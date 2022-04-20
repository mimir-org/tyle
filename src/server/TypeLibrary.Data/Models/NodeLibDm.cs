using System;
using System.Collections.Generic;
using Mimirorg.TypeLibrary.Enums;

namespace TypeLibrary.Data.Models
{
    public class NodeLibDm
    {
        public string Id { get; set; }
        public string Iri { get; set; }
        public string Name { get; set; }
        public string RdsId { get; set; }
        public string RdsName { get; set; }
        public string PurposeId { get; set; }
        public PurposeLibDm Purpose { get; set; }
        public string ParentId { get; set; }
        public NodeLibDm Parent { get; set; }
        public string Version { get; set; }
        public string FirstVersionId { get; set; }
        public Aspect Aspect { get; set; }
        public State State { get; set; }
        public int CompanyId { get; set; }
        public string Description { get; set; }
        public string BlobId { get; set; }
        public BlobLibDm Blob { get; set; }
        public string AttributeAspectId { get; set; }
        public AttributeAspectLibDm AttributeAspect { get; set; }

        public string UpdatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }

        public virtual ICollection<NodeLibDm> Children { get; set; }
        public virtual ICollection<CollectionLibDm> Collections { get; set; }
        public virtual ICollection<NodeTerminalLibDm> NodeTerminals { get; set; }
        public virtual ICollection<AttributeLibDm> Attributes { get; set; }
        public virtual ICollection<SimpleLibDm> Simples { get; set; }
        public virtual List<SelectedAttributePredefinedLibDm> SelectedAttributePredefined { get; set; }
    }
}
