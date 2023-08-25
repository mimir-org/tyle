namespace Mimirorg.TypeLibrary.Models.Client;

public class AttributeGroupLibCm
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ICollection<AttributeLibCm> Attributes { get; set; }
}
