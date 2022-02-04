using Mimirorg.Common.Enums;
using Newtonsoft.Json;

namespace Mimirorg.TypeLibrary.Models.Client
{
    public class TerminalLibCm
    {
        public string TerminalTypeId { get; set; }
        public int Number { get; set; }
        public ConnectorType ConnectorType { get; set; }

        [JsonIgnore]
        public string Key => $"{TerminalTypeId}-{ConnectorType}";
    }
}
