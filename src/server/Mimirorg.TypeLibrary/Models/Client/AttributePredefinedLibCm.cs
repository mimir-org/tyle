using Mimirorg.TypeLibrary.Enums;

namespace Mimirorg.TypeLibrary.Models.Client
{
    public class AttributePredefinedLibCm
    {
        public string Key { get; set; }
        public string Iri { get; set; }
        public ICollection<string> ContentReferences { get; set; }
        public bool IsMultiSelect { get; set; }
        public ICollection<string> ValueStringList { get; set; }
        public Aspect Aspect { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public string Kind => nameof(AttributePredefinedLibCm);
    }
}