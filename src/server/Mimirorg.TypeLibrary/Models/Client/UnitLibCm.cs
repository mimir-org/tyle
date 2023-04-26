using Mimirorg.Common.Contracts;
using Mimirorg.Common.Enums;
using Mimirorg.TypeLibrary.Models.Application;

namespace Mimirorg.TypeLibrary.Models.Client;

public class UnitLibCm : IStatefulObject, IEquatable<UnitLibAm>
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Iri { get; set; }
    public string TypeReference { get; set; }
    public DateTime Created { get; set; }
    public string CreatedBy { get; set; }
    public State State { get; set; }
    public string Symbol { get; set; }
    public string Description { get; set; }

    public string Kind => nameof(UnitLibCm);

    public bool Equals(UnitLibAm other)
    {
        if (other == null) return false;
        return this.Name == other.Name && this.Symbol == other.Symbol;
    }
}