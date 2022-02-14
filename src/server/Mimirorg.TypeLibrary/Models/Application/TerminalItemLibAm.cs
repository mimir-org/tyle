using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Extensions;

namespace Mimirorg.TypeLibrary.Models.Application
{
    public class TerminalItemLibAm
    {
        public string TerminalId { get; set; }
        public int Number { get; set; }
        public ConnectorDirection ConnectorDirection { get; set; }
        public string Key => $"{TerminalId}-{ConnectorDirection}".CreateMd5();
    }
}
