using Mimirorg.Common.Contracts;
using Mimirorg.Common.Enums;

namespace Mimirorg.TypeLibrary.Models.Client;

public class AttributeLibCm : IStatefulObject
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Iri { get; set; }
    public string TypeReference { get; set; }
    public DateTime Created { get; set; }
    public string CreatedBy { get; set; }
    public State State { get; set; }
    public string Description { get; set; }
    public ICollection<AttributeUnitLibCm> AttributeUnits { get; set; }

    public string Kind => nameof(AttributeLibCm);
}