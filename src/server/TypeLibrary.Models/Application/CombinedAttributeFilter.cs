using System.Collections.Generic;

namespace TypeLibrary.Models.Application
{
    public class CombinedAttributeFilter
    {
        public string Name { get; set; }
        public ICollection<CombinedAttribute> CombinedAttributes { get; set; }
    }
}
