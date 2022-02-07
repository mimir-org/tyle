using Mimirorg.Common.Enums;

namespace Mimirorg.TypeLibrary.Models.Data
{
    public class TerminalNodeLibDm
    {
        public string Id { get; set; }
        public int Number { get; set; }
        public ConnectorType ConnectorType { get; set; }
        public string NodeId { get; set; }
        public NodeLibDm Node { get; set; }
        public string TerminalId { get; set; }
        public TerminalLibDm Terminal { get; set; }
    }
}
