using Mimirorg.Common.Enums;

namespace Mimirorg.TypeLibrary.Models.Data
{
    public class NodeTerminalLibDm
    {
        public string Id { get; set; }
        public int Number { get; set; }
        public ConnectorType ConnectorType { get; set; }
        public string NodeTypeId { get; set; }
        public NodeLibDm NodeDm { get; set; }
        public string TerminalTypeId { get; set; }
        public TerminalLibDm TerminalDm { get; set; }
    }
}
