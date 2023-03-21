using Mimirorg.Common.Enums;
using Mimirorg.TypeLibrary.Enums;

namespace Mimirorg.TypeLibrary.Models.Client;

public class NodeLibCm
{
    public int Id { get; set; }
    public int? ParentId { get; set; }
    public string ParentName { get; set; }
    public string ParentIri { get; set; }
    public string Name { get; set; }
    public string Version { get; set; }
    public int FirstVersionId { get; set; }
    public string Iri { get; set; }
    public ICollection<TypeReferenceCm> TypeReferences { get; set; }
    public string RdsCode { get; set; }
    public string RdsName { get; set; }
    public string PurposeName { get; set; }
    public Aspect Aspect { get; set; }
    public State State { get; set; }
    public int CompanyId { get; set; }
    public string CompanyName { get; set; }
    public string Symbol { get; set; }
    public string Description { get; set; }
    public DateTime Created { get; set; }
    public string CreatedBy { get; set; }
    public ICollection<NodeLibCm> Children { get; set; }
    public ICollection<NodeTerminalLibCm> NodeTerminals { get; set; }
    public ICollection<NodeAttributeLibCm> NodeAttributes { get; set; }
    public ICollection<SelectedAttributePredefinedLibCm> SelectedAttributePredefined { get; set; }
    public string Kind => nameof(NodeLibCm);
}