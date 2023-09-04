using Mimirorg.Common.Contracts;
using Mimirorg.Common.Enums;
using Mimirorg.TypeLibrary.Enums;
using System;
using System.Collections.Generic;
using TypeLibrary.Data.Contracts.Common;

namespace TypeLibrary.Data.Models;

public class TerminalLibDm // : ILogable, IStatefulObject
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
    public ICollection<ClassifierReference> Classifiers { get; set; } = new List<ClassifierReference>();
    public PurposeReference? Purpose { get; set; }
    public string? Notation { get; set; }
    public string? Symbol { get; set; }
    public Aspect Aspect { get; set; }
    public MediumReference? Medium { get; set; }
    public Direction Qualifier { get; set; }
    public ICollection<BlockTerminalLibDm> TerminalBlocks { get; set; } = null!;
    public ICollection<TerminalAttributeLibDm> TerminalAttributes { get; set; } = null!;

    /*#region ILogable

    public LogLibDm CreateLog(LogType logType, string logTypeValue, string createdBy)
    {
        return new LogLibDm
        {
            ObjectId = Id,
            ObjectFirstVersionId = null,
            ObjectType = nameof(TerminalLibDm),
            ObjectName = Name,
            ObjectVersion = null,
            LogType = logType,
            LogTypeValue = logTypeValue,
            Created = DateTime.UtcNow,
            CreatedBy = createdBy
        };
    }

    #endregion ILogable*/
}