using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mimirorg.Common.Contracts;
using Mimirorg.Common.Enums;
using Mimirorg.TypeLibrary.Enums;
using TypeLibrary.Data.Contracts.Common;

namespace TypeLibrary.Data.Models
{
    public class AttributeGroupDm : IStatefulObject, ILogable
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Attribute { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public string Description { get; set; }
        public State State { get; set; }

        public LogLibDm CreateLog(LogType logType, string logTypeValue, string createdBy)
        {
            return new LogLibDm
            {
                ObjectId = Id,
                ObjectFirstVersionId = null,
                ObjectType = nameof(AttributeGroupDm),
                ObjectName = Name,
                ObjectVersion = null,
                LogType = logType,
                LogTypeValue = logTypeValue,
                Created = DateTime.UtcNow,
                CreatedBy = createdBy
            };
        }
    }
}