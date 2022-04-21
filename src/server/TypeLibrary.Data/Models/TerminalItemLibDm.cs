using Mimirorg.TypeLibrary.Enums;
using Newtonsoft.Json;

namespace TypeLibrary.Data.Models
{
    public class TerminalItemLibDm
    {
        public string TerminalId { get; set; }
        public int Number { get; set; }
        public ConnectorDirection ConnectorDirection { get; set; }

        [JsonIgnore]
        public string Key => $"{TerminalId}-{ConnectorDirection}";
    }
}
