using System.Collections.Generic;
using Newtonsoft.Json;
using TypeLibrary.Models.Data.TypeEditor;

namespace TypeLibrary.Models.Data.Enums
{
    
    public class TerminalCategory : EnumBase
    {
        public string Color { get; set; }

        //TODO: Remove refs to Terminals
        //[JsonIgnore]
        //public virtual ICollection<Terminal> Terminals { get; set; }

        [JsonIgnore]
        public virtual ICollection<TerminalType> TerminalTypes { get; set; }
    }
}
