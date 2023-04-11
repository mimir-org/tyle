using Mimirorg.Common.Enums;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using System;
using System.Collections.Generic;
using Mimirorg.Common.Contracts;
using TypeLibrary.Data.Contracts.Common;

namespace TypeLibrary.Data.Models;

public class AttributeLibDm : ILogable, IStatefulObject
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Iri { get; set; }
    public string TypeReference { get; set; }
    public DateTime Created { get; set; }
    public string CreatedBy { get; set; }
    public int? CompanyId { get; set; }
    public State State { get; set; }
    public string Description { get; set; }
    public ICollection<AttributeUnitLibDm> AttributeUnits { get; set; }
    public ICollection<AspectObjectAttributeLibDm> AttributeAspectObjects { get; set; }
    public ICollection<TerminalAttributeLibDm> AttributeTerminals { get; set; }

    public LogLibAm CreateLog(LogType logType, string logTypeValue, string comment)
    {
        return new LogLibAm
        {
            ObjectId = Id,
            ObjectFirstVersionId = Id,
            ObjectType = nameof(AttributeLibDm),
            ObjectName = Name,
            ObjectVersion = "",
            LogType = logType,
            LogTypeValue = logTypeValue,
            Comment = comment
        };
    }
}