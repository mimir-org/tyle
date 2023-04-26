using Mimirorg.Common.Contracts;
using Mimirorg.Common.Enums;
using Mimirorg.TypeLibrary.Enums;
using System;
using System.Collections.Generic;
using TypeLibrary.Data.Contracts.Common;

namespace TypeLibrary.Data.Models;

public class TerminalLibDm : IStatefulObject, ILogable
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Iri { get; set; }
    public string TypeReference { get; set; }
    public DateTime Created { get; set; }
    public string CreatedBy { get; set; }
    public int CompanyId { get; set; }
    public State State { get; set; }
    public string Color { get; set; }
    public string Description { get; set; }
    public string ParentId { get; set; }
    public TerminalLibDm Parent { get; set; }
    public ICollection<TerminalLibDm> Children { get; set; }
    public ICollection<AspectObjectTerminalLibDm> TerminalAspectObjects { get; set; }
    public ICollection<AttributeLibDm> Attributes { get; set; }
    public ICollection<TerminalAttributeLibDm> TerminalAttributes { get; set; }

    #region ILogable

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
            CreatedBy = createdBy
        };
    }

    #endregion ILogable
}