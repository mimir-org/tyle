using System.Collections.Generic;

namespace TypeLibrary.Data.Models
{
    public class InterfaceLibDm : LibraryTypeLibDm
    {
        public string TerminalId { get; set; }
        public TerminalLibDm Terminal { get; set; }
        public ICollection<AttributeLibDm> Attributes { get; set; }
    }
}
