using System.Collections.Generic;

namespace TypeLibrary.Models.Data
{
    public class PredefinedAttribute
    {
        public string Key { get; set; }
        public virtual ICollection<string> Values { get; set; }
        public bool IsMultiSelect { get; set; }
    }
}
