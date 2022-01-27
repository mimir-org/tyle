using System.Collections.Generic;

namespace TypeLibrary.Models.Models.Client
{
    public class TypeCm
    {
        public IEnumerable<NodeCm> ObjectBlocks { get; set; }
        public IEnumerable<InterfaceCm> Interfaces { get; set; }
        public IEnumerable<TransportCm> Transports { get; set; }
        public IEnumerable<SubProjectCm> SubProjects { get; set; }
    }
}
