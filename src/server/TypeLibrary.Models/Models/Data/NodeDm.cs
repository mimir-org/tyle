using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;
using TypeLibrary.Models.Models.Client;

namespace TypeLibrary.Models.Models.Data
{
    public class NodeDm : TypeDm
    {
        public ICollection<NodeTerminalDm> TerminalTypes { get; set; }
        public ICollection<AttributeDm> AttributeList { get; set; }
        public string LocationType { get; set; }
        public string SymbolId { get; set; }
        public virtual ICollection<SimpleDm> SimpleTypes { get; set; }
        [NotMapped]
        public ICollection<AttributePredefinedCm> PredefinedAttributes { get; set; }

        [JsonIgnore]
        public string PredefinedAttributeData { get; set; }

        public void ResolvePredefinedAttributeData()
        {
            if (PredefinedAttributes == null || !PredefinedAttributes.Any())
            {
                PredefinedAttributeData = null;
                return;
            }

            PredefinedAttributeData = JsonConvert.SerializeObject(PredefinedAttributes);
        }

        public void ResolvePredefinedAttributes()
        {
            if (string.IsNullOrEmpty(PredefinedAttributeData))
            {
                PredefinedAttributes = null;
                return;
            }

            PredefinedAttributes = JsonConvert.DeserializeObject<ICollection<AttributePredefinedCm>>(PredefinedAttributeData);
        }
    }
}
