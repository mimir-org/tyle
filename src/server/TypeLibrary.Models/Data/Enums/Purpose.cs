using System.Collections.Generic;
using Newtonsoft.Json;
using TypeLibrary.Models.Data.TypeEditor;
using TypeLibrary.Models.Enums;

namespace TypeLibrary.Models.Data.Enums
{
    public class Purpose : EnumBase
    {
        public Discipline Discipline { get; set; }
        public override string Key => $"{Name}-{InternalType}-{Discipline}";
        
        [JsonIgnore]
        public virtual ICollection<LibraryType> LibraryTypes { get; set; }
    }
}
