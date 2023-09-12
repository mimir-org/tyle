using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Models.Client;

namespace Mimirorg.TypeLibrary.Models.Application
{
    public class AttributeGroupAttributesLibAm
    {
        public string Id { get; set; }
        public string AttributeGroupId { get; set; }
        public AttributeGroupLibCm AttributeGroup { get; set; }
        public string AttributeId { get; set; }
        public AttributeLibCm Attribute { get; set; }
    }
}
