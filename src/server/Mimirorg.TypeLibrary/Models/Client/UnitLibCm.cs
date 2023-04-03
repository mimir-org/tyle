using Mimirorg.Common.Enums;

namespace Mimirorg.TypeLibrary.Models.Client;

public class UnitLibCm
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Iri { get; set; }
    public string TypeReference { get; set; }
    public DateTime Created { get; set; }
    public string CreatedBy { get; set; }
    public int? CompanyId { get; set; }
    public string CompanyName { get; set; }
    public State State { get; set; }
    public string Symbol { get; set; }
    public string Description { get; set; }

    public string Kind => nameof(UnitLibCm);
}