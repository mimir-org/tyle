using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Enums;

namespace TypeLibrary.Data.Models
{
    public class AttributeGroupDm
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public string Description { get; set; }


        //TODO What is this?

        //public LogLibDm CreateLog(LogType logType, string logTypeValue, string createdBy)
        //{
        //    return new LogLibDm
        //    {
        //        ObjectId = Id,
        //        ObjectFirstVersionId = null,
        //        ObjectType = nameof(AttributeLibDm),
        //        ObjectName = Name,
        //        ObjectVersion = null,
        //        LogType = logType,
        //        LogTypeValue = logTypeValue,
        //        Created = DateTime.UtcNow,
        //        CreatedBy = createdBy
        //    };
        //}
    }
}