using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;
using TypeLibrary.Models.Application;

namespace TypeLibrary.Models.Data
{
    public class NodeType : LibraryType
    {
        public ICollection<NodeTypeTerminalType> TerminalTypes { get; set; }
        public ICollection<Attribute> AttributeList { get; set; }
        public string LocationType { get; set; }
        public string SymbolId { get; set; }
        public virtual ICollection<SimpleType> SimpleTypes { get; set; }
        [NotMapped]
        public ICollection<Application.PredefinedAttribute> PredefinedAttributes { get; set; }

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

            PredefinedAttributes = JsonConvert.DeserializeObject<ICollection<Application.PredefinedAttribute>>(PredefinedAttributeData);
        }
    }
}
