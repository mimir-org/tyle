using System.Collections.Generic;
using Newtonsoft.Json;
using TypeLibrary.Models.Data.TypeEditor;

namespace TypeLibrary.Models.Data.Enums
{
    public class RdsCategory : EnumBase
    {
        [JsonIgnore]
        public virtual ICollection<Rds> RdsList { get; set; }
    }
}
