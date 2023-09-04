using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mimirorg.TypeLibrary.Models.Application
{
    public class AttributeGroupAm
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public List<string> AttributeId  { get; set; }

        public string TypeReference { get; set; }

        public string Description { get; set; }

        public ICollection<AttributeUnitLibAm> AttributeUnits { get; set; }
    }
}
