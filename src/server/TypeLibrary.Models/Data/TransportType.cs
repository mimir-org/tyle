using System.Collections.Generic;

namespace TypeLibrary.Models.Data
{
    public class TransportType : LibraryType
    {
        public string TerminalTypeId { get; set; }
        public TerminalType TerminalType { get; set; }
        public ICollection<Attribute> AttributeList { get; set; }
    }
}
