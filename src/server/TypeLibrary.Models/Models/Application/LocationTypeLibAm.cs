using System.Collections.Generic;

namespace TypeLibrary.Models.Models.Application
{
    public class LocationTypeLibAm
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual string SemanticReference { get; set; }
        public ICollection<LocationTypeLibAm> LocationSubTypes { get; set; }
    }
}
