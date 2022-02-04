namespace Mimirorg.TypeLibrary.Models.Client
{
    public class TypeLibCm
    {
        public IEnumerable<NodeLibCm> ObjectBlocks { get; set; }
        public IEnumerable<InterfaceLibCm> Interfaces { get; set; }
        public IEnumerable<TransportLibCm> Transports { get; set; }
        public IEnumerable<SubProjectLibCm> SubProjects { get; set; }
    }
}
