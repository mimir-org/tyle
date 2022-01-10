using System.Collections.Generic;
using TypeLibrary.Models.Data.Enums;

namespace TypeLibrary.Models.Data.TypeEditor
{
    public class TerminalType
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        
        public string TerminalCategoryId { get; set; }
        public TerminalCategory TerminalCategory { get; set; }
        
        public string SemanticReference { get; set; }
        public ICollection<AttributeType> Attributes { get; set; }
        public ICollection<NodeTypeTerminalType> NodeTypes { get; set; }
        public ICollection<InterfaceType> InterfaceTypes { get; set; }
        public ICollection<TransportType> TransportTypes { get; set; }
    }
}
