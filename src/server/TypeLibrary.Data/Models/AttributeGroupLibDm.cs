using AngleSharp.Text;
using Mimirorg.Common.Contracts;
using Mimirorg.Common.Enums;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection.Metadata;
using TypeLibrary.Data.Contracts.Common;


namespace TypeLibrary.Data.Models;

    public class AttributeGroupLibDm : ILogable
{
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<AttributeGroupAttributesLibDm> Attributes { get; set; }
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
                ObjectType = nameof(AttributeGroupLibDm),
                ObjectName = Name,
                ObjectVersion = null,
                LogType = logType,
                LogTypeValue = logTypeValue,
                Created = DateTime.UtcNow,
                CreatedBy = createdBy
            };
        }


}