using System.Collections.Generic;

namespace TypeLibrary.Models.Models.Data
{
    public class AttributePredefinedDm
    {
        public string Key { get; set; }
        public virtual ICollection<string> Values { get; set; }
        public bool IsMultiSelect { get; set; }
    }
}