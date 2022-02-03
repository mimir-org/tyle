using System.Collections.Generic;

namespace TypeLibrary.Models.Models.Data
{
    public class InterfaceLibDm : TypeLibDm
    {
        public string TerminalId { get; set; }
        public TerminalLibDm TerminalDm { get; set; }
        public ICollection<AttributeLibDm> AttributeList { get; set; }
    }
}
