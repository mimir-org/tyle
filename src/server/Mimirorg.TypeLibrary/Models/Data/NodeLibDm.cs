using System.ComponentModel.DataAnnotations.Schema;
using Mimirorg.TypeLibrary.Models.Client;
using Newtonsoft.Json;

namespace Mimirorg.TypeLibrary.Models.Data
{
    public class NodeLibDm : LibraryTypeLibDm
    {
        public ICollection<TerminalNodeLibDm> TerminalNodes { get; set; }
        public ICollection<AttributeLibDm> Attributes { get; set; }
        public string LocationType { get; set; }
        public string SymbolId { get; set; }
        public virtual ICollection<SimpleLibDm> SimpleTypes { get; set; }
        [NotMapped]
        public ICollection<AttributePredefinedLibCm> AttributesPredefined { get; set; }

        [JsonIgnore]
        public string AttributeDataPredefined { get; set; }

        public void ResolveAttributeDataPredefined()
        {
            if (AttributesPredefined == null || !AttributesPredefined.Any())
            {
                AttributeDataPredefined = null;
                return;
            }

            AttributeDataPredefined = JsonConvert.SerializeObject(AttributesPredefined);
        }

        public void ResolveAttributesPredefined()
        {
            if (string.IsNullOrEmpty(AttributeDataPredefined))
            {
                AttributesPredefined = null;
                return;
            }

            AttributesPredefined = JsonConvert.DeserializeObject<ICollection<AttributePredefinedLibCm>>(AttributeDataPredefined);
        }
    }
}
