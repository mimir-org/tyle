namespace Mimirorg.TypeLibrary.Models.Client
{
    public class AttributeGroupLibCm
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public string Description { get; set; }        
        public ICollection<AttributeLibCm> Attributes { get; set; }
    }
}