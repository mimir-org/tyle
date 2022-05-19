using System;
using System.Collections.Generic;
using Mimirorg.TypeLibrary.Enums;

namespace TypeLibrary.Data.Models
{
    public class AttributePredefinedLibDm
    {
        public string Key { get; set; }
        public string Iri { get; set; }
        public string ContentReferences { get; set; }
        public bool IsMultiSelect { get; set; }
        public ICollection<string> ValueStringList { get; set; }
        public Aspect Aspect { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public bool Deleted { get; set; }
    }
}