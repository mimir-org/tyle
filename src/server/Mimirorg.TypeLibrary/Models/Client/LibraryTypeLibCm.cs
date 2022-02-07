namespace Mimirorg.TypeLibrary.Models.Client
{
    public class LibraryTypeLibCm
    {
        public IEnumerable<NodeLibCm> Nodes { get; set; }
        public IEnumerable<InterfaceLibCm> Interfaces { get; set; }
        public IEnumerable<TransportLibCm> Transports { get; set; }
        public IEnumerable<SubProjectLibCm> SubProjects { get; set; }
    }
}
