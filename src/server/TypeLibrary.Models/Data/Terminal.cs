using System.Collections.Generic;
using Newtonsoft.Json;
using TypeLibrary.Models.Data.Enums;

namespace TypeLibrary.Models.Data
{
    public class Terminal : Connector
    {
        public string Color { get; set; }
        public string TerminalCategoryId { get; set; }
        public TerminalCategory TerminalCategory { get; set; }
        public string TerminalTypeId { get; set; }

        public virtual ICollection<Attribute> Attributes { get; set; }

        [JsonIgnore]
        public ICollection<Transport> InputTransports { get; set; }

        [JsonIgnore]
        public ICollection<Transport> OutputTransports { get; set; }

        [JsonIgnore]
        public ICollection<Interface> InputInterfaces { get; set; }

        [JsonIgnore]
        public ICollection<Interface> OutputInterfaces { get; set; }
    }
}
