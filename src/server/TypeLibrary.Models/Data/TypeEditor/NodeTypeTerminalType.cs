using TypeLibrary.Models.Enums;

namespace TypeLibrary.Models.Data.TypeEditor
{
    public class NodeTypeTerminalType
    {
        public string Id { get; set; }
        public int Number { get; set; }
        public ConnectorType ConnectorType { get; set; }
        public string NodeTypeId { get; set; }
        public NodeType NodeType { get; set; }
        public string TerminalTypeId { get; set; }
        public TerminalType TerminalType { get; set; }
    }
}
