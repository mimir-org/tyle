using System.ComponentModel.DataAnnotations;
using Mimirorg.TypeLibrary.Enums;

namespace Mimirorg.TypeLibrary.Models.Client
{
    public class AttributePredefinedLibCm
    {
        public string Key { get; set; }
        public string Iri { get; set; }
        public bool IsMultiSelect { get; set; }
        public ICollection<string> ValueStringList { get; set; }
        public Aspect Aspect { get; set; }
        public string Kind => nameof(AttributePredefinedLibCm);
    }
}
