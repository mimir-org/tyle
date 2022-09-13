using Mimirorg.TypeLibrary.Enums;

namespace Mimirorg.TypeLibrary.Models.Client
{
    public class RdsLibCm
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Iri { get; set; }
        public ICollection<TypeReferenceCm> TypeReferences { get; set; }
        public State State { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public string Kind => nameof(RdsLibCm);
    }
}