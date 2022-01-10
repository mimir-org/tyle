using System.Collections.Generic;

namespace TypeLibrary.Models.Application.TypeEditor
{
    public class PredefinedAttributeAm
    {
        public string Key { get; set; }
        public virtual Dictionary<string, bool> Values { get; set; }
        public bool IsMultiSelect { get; set; }
    }
}
