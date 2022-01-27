using TypeLibrary.Models.Enums;

namespace TypeLibrary.Models.Models.Data
{
    public class NodeTerminalDm
    {
        public string Id { get; set; }
        public int Number { get; set; }
        public ConnectorType ConnectorType { get; set; }
        public string NodeTypeId { get; set; }
        public NodeDm NodeDm { get; set; }
        public string TerminalTypeId { get; set; }
        public TerminalDm TerminalDm { get; set; }
    }
}
