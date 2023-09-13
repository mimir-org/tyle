using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeLibrary.Data.Models
{
    public class AttributeGroupAttributesLibDm
    {
        public string Id { get; set; }
        public string AttributeGroupId { get; set; }
        public AttributeGroupLibDm AttributeGroup { get; set; }
        public string AttributeId { get; set; }
        public AttributeLibDm Attribute { get; set; }
    }
}