using Mimirorg.Common.Contracts;
using Mimirorg.Common.Enums;
using Mimirorg.TypeLibrary.Enums;
using System;
using System.Collections.Generic;
using TypeLibrary.Data.Contracts.Common;

namespace TypeLibrary.Data.Models;

public class TerminalLibDm // : ILogable, IStatefulObject
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
    public List<string> Classifiers { get; set; }
    public string Purpose { get; set; }
    public string Notation { get; set; }
    public string Symbol { get; set; }
    public Aspect Aspect { get; set; }
    public string Medium { get; set; }
    public Direction Qualifier { get; set; }
    public ICollection<BlockTerminalLibDm> TerminalBlocks { get; set; }
    public ICollection<TerminalAttributeLibDm> TerminalAttributes { get; set; }

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