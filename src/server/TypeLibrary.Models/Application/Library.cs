using System.Collections.Generic;

namespace TypeLibrary.Models.Application
{
    public class Library
    {
        public IEnumerable<LibraryNodeItem> ObjectBlocks { get; set; }
        public IEnumerable<LibraryInterfaceItem> Interfaces { get; set; }
        public IEnumerable<LibraryTransportItem> Transports { get; set; }
        public IEnumerable<LibrarySubProjectItem> SubProjects { get; set; }
    }
}
