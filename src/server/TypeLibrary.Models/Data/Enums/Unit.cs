using System.Collections.Generic;
using Newtonsoft.Json;
using TypeLibrary.Models.Data.TypeEditor;

namespace TypeLibrary.Models.Data.Enums
{
    public class Unit : EnumBase
    {
        [JsonIgnore]
        public virtual ICollection<AttributeType> AttributeTypes { get; set; }
    }
}
