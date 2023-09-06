using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeLibrary.Data.Models
{
    public class AttributeGroupAttributesLibDm
    {
        public int Id { get; set; }
        public string AttributeGroupId { get; set; }
        public AttributeGroupLibDm AttributeGroup { get; set; }
        public string AttributeId { get; set; }
        public ICollection<AttributeLibDm> Attribute { get; set; }
    }
}
