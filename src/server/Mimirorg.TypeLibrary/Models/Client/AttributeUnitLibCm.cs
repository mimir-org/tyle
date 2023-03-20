namespace Mimirorg.TypeLibrary.Models.Client;

public class AttributeUnitLibCm
{
    public int Id { get; set; }
    public bool IsDefault { get; set; }
    public UnitLibCm Unit { get; set; }
    public string Kind => nameof(AttributeUnitLibCm);
}