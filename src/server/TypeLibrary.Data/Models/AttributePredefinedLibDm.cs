using System;
using System.Collections.Generic;
using Mimirorg.Common.Enums;
using Mimirorg.TypeLibrary.Enums;
// ReSharper disable InconsistentNaming

namespace TypeLibrary.Data.Models
{
    public class AttributePredefinedLibDm
    {
        public string Key { get; set; }
        public string Iri { get; set; }
        public string TypeReferences { get; set; }
        public bool IsMultiSelect { get; set; }
        public ICollection<string> ValueStringList { get; set; }
        public Aspect Aspect { get; set; }
        public State State { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }

    }
}