using System.Collections.Generic;

namespace TypeLibrary.Models.Data
{
    public class InterfaceType : LibraryType
    {
        public string TerminalTypeId { get; set; }
        public TerminalType TerminalType { get; set; }
        public ICollection<AttributeType> AttributeTypes { get; set; }
    }
}
