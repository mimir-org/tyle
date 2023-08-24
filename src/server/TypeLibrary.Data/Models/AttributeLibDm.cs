using Mimirorg.Common.Contracts;
using Mimirorg.Common.Enums;
using Mimirorg.TypeLibrary.Enums;
using System;
using System.Collections.Generic;
using TypeLibrary.Data.Contracts.Common;

namespace TypeLibrary.Data.Models;

public class AttributeLibDm // : ILogable, IStatefulObject
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Version { get; set; }
    public DateTimeOffset CreatedOn { get; set; }
    public string CreatedBy { get; set; }
    public List<string> ContributedBy { get; set; }
    public DateTimeOffset LastUpdateOn { get; set; }
    //public int CompanyId { get; set; }
    //public State State { get; set; }
    public string Predicate { get; set; }
    public List<string> UoMs { get; set; }
    public List<AttributeQualifier> Qualifiers { get; set; }
    public ValueConstraintLibDm ValueConstraint { get; set; }

    public ICollection<AttributeUnitLibDm> AttributeUnits { get; set; }
    public ICollection<BlockAttributeLibDm> AttributeBlocks { get; set; }
    public ICollection<TerminalAttributeLibDm> AttributeTerminals { get; set; }

    /*public LogLibDm CreateLog(LogType logType, string logTypeValue, string createdBy)
    {
        return new LogLibDm
        {
            ObjectId = Id,
            ObjectFirstVersionId = null,
            ObjectType = nameof(AttributeLibDm),
            ObjectName = Name,
            ObjectVersion = null,
            LogType = logType,
            LogTypeValue = logTypeValue,
            Created = DateTime.UtcNow,
            CreatedBy = createdBy
        };
    }*/
}