using Mimirorg.TypeLibrary.Enums;

namespace Mimirorg.TypeLibrary.Models.Client
{
    public class NodeLibCm
    {
        public string Id { get; set; }
        public string Iri { get; set; }
        public string Name { get; set; }
        public string RdsId { get; set; }
        public string RdsName { get; set; }
        public string PurposeId { get; set; }
        public string PurposeName { get; set; }
        public string ParentId { get; set; }
        public InterfaceLibCm Parent { get; set; }
        public string Version { get; set; }
        public string FirstVersionId { get; set; }
        public Aspect Aspect { get; set; }
        public State State { get; set; }
        public int CompanyId { get; set; }
        public string Description { get; set; }
        public string BlobId { get; set; }
        public BlobLibCm Blob { get; set; }
        public string AttributeAspectId { get; set; }
        public AttributeAspectLibCm AttributeAspect { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public ICollection<AttributeLibCm> Attributes { get; set; }
        public ICollection<NodeTerminalLibCm> NodeTerminals { get; set; }
        public ICollection<SimpleLibCm> Simples { get; set; }
        public ICollection<SelectedAttributePredefinedLibCm> SelectedAttributePredefined { get; set; }
        public string Kind => nameof(NodeLibCm);
    }
}
