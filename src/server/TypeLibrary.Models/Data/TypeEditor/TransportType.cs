using System.Collections.Generic;

namespace TypeLibrary.Models.Data.TypeEditor
{
    public class TransportType : LibraryType
    {
        public string TerminalTypeId { get; set; }
        public TerminalType TerminalType { get; set; }
        public ICollection<AttributeType> AttributeTypes { get; set; }
    }
}
