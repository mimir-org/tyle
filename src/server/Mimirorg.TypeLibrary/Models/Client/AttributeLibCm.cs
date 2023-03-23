using Mimirorg.Common.Enums;

namespace Mimirorg.TypeLibrary.Models.Client;

public class AttributeLibCm
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Iri { get; set; }
    public ICollection<TypeReferenceCm> TypeReferences { get; set; }
    public State State { get; set; }
    public int? CompanyId { get; set; }
    public string? CompanyName { get; set; }
    public string Description { get; set; }
    public DateTime Created { get; set; }
    public string CreatedBy { get; set; }
    public ICollection<AttributeUnitLibCm> AttributeUnits { get; set; }

    public string Kind => nameof(AttributeLibCm);
}