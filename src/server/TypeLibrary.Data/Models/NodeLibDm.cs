using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Mimirorg.TypeLibrary.Models.Client;
using Newtonsoft.Json;

namespace TypeLibrary.Data.Models
{
    public class NodeLibDm : LibraryTypeLibDm
    {
        public ICollection<TerminalNodeLibDm> TerminalNodes { get; set; }
        public ICollection<AttributeLibDm> Attributes { get; set; }
        public string AttributeAspectId { get; set; }
        public string BlobId { get; set; }
        public virtual ICollection<SimpleLibDm> Simple { get; set; }
        [NotMapped]
        public ICollection<AttributePredefinedLibCm> AttributesPredefined { get; set; }

        [JsonIgnore]
        public string AttributePredefined { get; set; }

        public void ResolveAttributePredefined()
        {
            if (AttributesPredefined == null || !AttributesPredefined.Any())
            {
                AttributePredefined = null;
                return;
            }

            AttributePredefined = JsonConvert.SerializeObject(AttributesPredefined);
        }

        public void ResolveAttributesPredefined()
        {
            if (string.IsNullOrEmpty(AttributePredefined))
            {
                AttributesPredefined = null;
                return;
            }

            AttributesPredefined = JsonConvert.DeserializeObject<ICollection<AttributePredefinedLibCm>>(AttributePredefined);
        }
    }
}
