using System.Collections.Generic;

namespace TypeLibrary.Models.Models.Client
{
    public class PredefinedAttributeCm
    {
        public string Key { get; set; }
        public virtual Dictionary<string, bool> Values { get; set; }
        public bool IsMultiSelect { get; set; }
    }
}
