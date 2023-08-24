using Mimirorg.Common.Contracts;
using Mimirorg.Common.Enums;
using Mimirorg.TypeLibrary.Enums;
using System;
using System.Collections.Generic;
using TypeLibrary.Data.Contracts.Common;

namespace TypeLibrary.Data.Models;

public class AttributeLibDm // : ILogable, IStatefulObject
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string Version { get; set; } = "1.0";
    public DateTimeOffset CreatedOn { get; set; }
    public required string CreatedBy { get; set; }
    public ICollection<string> ContributedBy { get; set; } = new List<string>();
    public DateTimeOffset LastUpdateOn { get; set; }
    //public int CompanyId { get; set; }
    //public State State { get; set; }
    public string? Predicate { get; set; }
    public ICollection<string> UoMs { get; set; } = new List<string>();
    public ProvenanceQualifier? ProvenanceQualifier { get; set; }
    public RangeQualifier? RangeQualifier { get; set; }
    public RegularityQualifier? RegularityQualifier { get; set; }
    public ScopeQualifier? ScopeQualifier { get; set; }
    public ValueConstraintLibDm? ValueConstraint { get; set; }

    //public ICollection<AttributeUnitLibDm> AttributeUnits { get; set; } = new List<AttributeUnitLibDm>();
    public ICollection<BlockAttributeLibDm> AttributeBlocks { get; set; } = new List<BlockAttributeLibDm>();
    public ICollection<TerminalAttributeLibDm> AttributeTerminals { get; set; } = new List<TerminalAttributeLibDm>();

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