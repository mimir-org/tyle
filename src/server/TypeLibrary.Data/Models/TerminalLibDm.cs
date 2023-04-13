using System;
using System.Collections.Generic;
using Mimirorg.Common.Contracts;
using Mimirorg.Common.Enums;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
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

    public LogLibAm CreateLog(LogType logType, string logTypeValue, string comment)
    {
        return new LogLibAm
        {
            ObjectId = Id,
            ObjectFirstVersionId = Id,
            ObjectType = nameof(TerminalLibDm),
            ObjectName = Name,
            ObjectVersion = "",
            LogType = logType,
            LogTypeValue = logTypeValue,
            Comment = comment
        };
    }

    #endregion ILogable
}