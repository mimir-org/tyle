using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;
using TypeLibrary.Models.Application.TypeEditor;

namespace TypeLibrary.Models.Data.TypeEditor
{
    public class NodeType : LibraryType
    {
        public ICollection<NodeTypeTerminalType> TerminalTypes { get; set; }
        public ICollection<AttributeType> AttributeTypes { get; set; }
        public string LocationType { get; set; }
        public string SymbolId { get; set; }
        public virtual ICollection<SimpleType> SimpleTypes { get; set; }

        [NotMapped]
        public ICollection<PredefinedAttributeAm> PredefinedAttributes { get; set; }

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

            PredefinedAttributes = JsonConvert.DeserializeObject<ICollection<PredefinedAttributeAm>>(PredefinedAttributeData);
        }
    }
}
