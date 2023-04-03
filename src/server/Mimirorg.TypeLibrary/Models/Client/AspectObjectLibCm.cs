using Mimirorg.Common.Enums;
using Mimirorg.TypeLibrary.Enums;

namespace Mimirorg.TypeLibrary.Models.Client;

public class AspectObjectLibCm
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Iri { get; set; }
    public string TypeReference { get; set; }
    public string Version { get; set; }
    public string FirstVersionId { get; set; }
    public DateTime Created { get; set; }
    public string CreatedBy { get; set; }
    public int CompanyId { get; set; }
    public string CompanyName { get; set; }
    public State State { get; set; }
    public Aspect Aspect { get; set; }
    public string PurposeName { get; set; }
    public string RdsCode { get; set; }
    public string RdsName { get; set; }
    public string Symbol { get; set; }
    public string Description { get; set; }
    public string ParentId { get; set; }
    public string ParentName { get; set; }
    public string ParentIri { get; set; }
    public ICollection<AspectObjectLibCm> Children { get; set; }
    public ICollection<AspectObjectTerminalLibCm> NodeTerminals { get; set; }
    public ICollection<AspectObjectAttributeLibCm> NodeAttributes { get; set; }
    public ICollection<SelectedAttributePredefinedLibCm> SelectedAttributePredefined { get; set; }
    public string Kind => nameof(AspectObjectLibCm);
}