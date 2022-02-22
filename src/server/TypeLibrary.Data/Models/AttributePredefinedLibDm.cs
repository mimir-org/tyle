using System.Collections.Generic;
using Mimirorg.TypeLibrary.Enums;

namespace TypeLibrary.Data.Models
{
    public class AttributePredefinedLibDm
    {
        public string Key { get; set; }
        public string Iri { get; set; }
        public bool IsMultiSelect { get; set; }
        public virtual ICollection<string> ValueStringList { get; set; }
        public Aspect Aspect { get; set; }
    }
}