using Mimirorg.TypeLibrary.Enums;

namespace Mimirorg.TypeLibrary.Models.Client
{
    public class SelectedAttributePredefinedLibCm
    {
        public string Key { get; set; }
        public string Iri { get; set; }
        public ICollection<string> ContentReferences { get; set; }
        public bool IsMultiSelect { get; set; }
        public virtual Dictionary<string, bool> Values { get; set; }
        public Aspect Aspect { get; set; }

        public string Kind => nameof(SelectedAttributePredefinedLibCm);
    }
}