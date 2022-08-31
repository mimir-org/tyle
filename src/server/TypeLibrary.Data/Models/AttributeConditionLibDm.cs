using TypeLibrary.Data.Contracts.Common;

namespace TypeLibrary.Data.Models
{
    public class AttributeConditionLibDm : IDatum
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Iri { get; set; }
        public string TypeReferences { get; set; }
        public string Description { get; set; }
    }
}