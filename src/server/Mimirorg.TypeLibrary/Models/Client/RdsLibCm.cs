using Mimirorg.TypeLibrary.Enums;

namespace Mimirorg.TypeLibrary.Models.Client
{
    public class RdsLibCm
    {
        public string Id { get; set; }
        public string RdsCategoryId { get; set; }
        public string Name { get; set; }
        public string Iri { get; set; }
        public string Code { get; set; }
        public Aspect Aspect { get; set; }

        public string Kind => nameof(RdsLibCm);
    }
}
