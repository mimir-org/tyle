using Mimirorg.Common.Contracts;
using Mimirorg.Common.Enums;
using Mimirorg.TypeLibrary.Enums;
using System;
using System.Collections.Generic;
using TypeLibrary.Data.Contracts.Common;
// ReSharper disable InconsistentNaming

namespace TypeLibrary.Data.Models;

public class AttributePredefinedLibDm : IStatefulObject, ILogable
{
    public string Key { get; set; }
    public string Iri { get; set; }
    public string TypeReference { get; set; }
    public DateTime Created { get; set; }
    public string CreatedBy { get; set; }
    public State State { get; set; }
    public Aspect Aspect { get; set; }
    public bool IsMultiSelect { get; set; }
    public ICollection<string> ValueStringList { get; set; }
    public LogLibDm CreateLog(LogType logType, string logTypeValue, string createdBy)
    {
        return new LogLibDm
        {
            ObjectId = Key,
            ObjectFirstVersionId = null,
            ObjectType = nameof(AttributePredefinedLibDm),
            ObjectName = Key,
            ObjectVersion = null,
            LogType = logType,
            LogTypeValue = logTypeValue,
            CreatedBy = createdBy
        };
    }
}