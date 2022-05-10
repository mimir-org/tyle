using System.Collections.Generic;
using Mimirorg.TypeLibrary.Enums;

namespace TypeLibrary.Data.Models
{
    public class SelectedAttributePredefinedLibDm
    {
        public string Key { get; set; }
        public string Iri { get; set; }
        public string ContentReferences { get; set; }
        public bool IsMultiSelect { get; set; }
        public virtual Dictionary<string, bool> Values { get; set; }
        public Aspect Aspect { get; set; }
    }
}
