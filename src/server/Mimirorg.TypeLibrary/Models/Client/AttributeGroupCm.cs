using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mimirorg.Common.Enums;

namespace Mimirorg.TypeLibrary.Models.Client
{
    public class AttributeGroupCm
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string TypeReference { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public State State { get; set; }
        public string Description { get; set; }
    }
}
