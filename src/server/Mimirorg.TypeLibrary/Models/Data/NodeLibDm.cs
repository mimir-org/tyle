using System.ComponentModel.DataAnnotations.Schema;
using Mimirorg.TypeLibrary.Models.Client;
using Newtonsoft.Json;

namespace Mimirorg.TypeLibrary.Models.Data
{
    public class NodeLibDm : TypeLibDm
    {
        public ICollection<NodeTerminalLibDm> TerminalTypes { get; set; }
        public ICollection<AttributeLibDm> AttributeList { get; set; }
        public string LocationType { get; set; }
        public string SymbolId { get; set; }
        public virtual ICollection<SimpleLibDm> SimpleTypes { get; set; }
        [NotMapped]
        public ICollection<AttributePredefinedLibCm> PredefinedAttributes { get; set; }

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

            PredefinedAttributes = JsonConvert.DeserializeObject<ICollection<AttributePredefinedLibCm>>(PredefinedAttributeData);
        }
    }
}
