using Mimirorg.Common.Enums;
using Newtonsoft.Json;

namespace Mimirorg.TypeLibrary.Models.Client
{
    public class TerminalLibCm
    {
        public string TerminalId { get; set; }
        public int Number { get; set; }
        public ConnectorDirection ConnectorDirection { get; set; }

        [JsonIgnore]
        public string Key => $"{TerminalId}-{ConnectorDirection}";
    }
}
