using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Extensions;

namespace Mimirorg.TypeLibrary.Models.Application
{
    public class NodeTerminalLibAm
    {
        public string TerminalId { get; set; }
        public int Number { get; set; }
        public ConnectorDirection ConnectorDirection { get; set; }
        public string Id => $"{TerminalId}-{ConnectorDirection}".CreateMd5();
    }
}
