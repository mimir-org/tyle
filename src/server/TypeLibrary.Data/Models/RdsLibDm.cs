using System;
using Mimirorg.Common.Contracts;
using Mimirorg.Common.Enums;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using TypeLibrary.Data.Contracts.Common;

namespace TypeLibrary.Data.Models;

public class RdsLibDm : ILogable, IStatefulObject
{
    public string Id { get; set; }
    public string RdsCode { get; set; }
    public string Name { get; set; }
    public string Iri { get; set; }
    public string TypeReference { get; set; }
    public DateTime Created { get; set; }
    public string CreatedBy { get; set; }
    public int? CompanyId { get; set; }
    public State State { get; set; }
    public string Description { get; set; }
    public string CategoryId { get; set; }
    public CategoryLibDm Category { get; set; }

    public LogLibAm CreateLog(LogType logType, string logTypeValue, string comment)
    {
        return new LogLibAm
        {
            ObjectId = Id,
            ObjectFirstVersionId = Id,
            ObjectType = nameof(RdsLibDm),
            ObjectName = Name,
            ObjectVersion = "",
            LogType = logType,
            LogTypeValue = logTypeValue,
            Comment = comment
        };
    }
}