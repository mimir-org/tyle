using System.Collections.Generic;

namespace TypeLibrary.Data.Models
{
    public class TransportLibDm : LibraryTypeLibDm
    {
        public string TerminalId { get; set; }
        public TerminalLibDm Terminal { get; set; }
        public ICollection<AttributeLibDm> Attributes { get; set; }
    }
}
