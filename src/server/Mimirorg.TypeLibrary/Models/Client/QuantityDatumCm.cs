using Mimirorg.TypeLibrary.Enums;

namespace Mimirorg.TypeLibrary.Models.Client;

public class QuantityDatumCm
{
    public string Name { get; set; }
    public string Source { get; set; }
    public string Iri { get; set; }
    public string Description { get; set; }
    public QuantityDatumType QuantityDatumType { get; set; }
}