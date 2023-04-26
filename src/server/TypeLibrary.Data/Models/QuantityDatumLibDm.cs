using Mimirorg.Common.Contracts;
using Mimirorg.Common.Enums;
using Mimirorg.TypeLibrary.Enums;
using System;
using TypeLibrary.Data.Contracts.Common;

namespace TypeLibrary.Data.Models;

public class QuantityDatumLibDm : ILogable, IStatefulObject
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Iri { get; set; }
    public string TypeReference { get; set; }
    public DateTime Created { get; set; }
    public string CreatedBy { get; set; }
    public State State { get; set; }
    public QuantityDatumType QuantityDatumType { get; set; }
    public string Description { get; set; }


    public LogLibDm CreateLog(LogType logType, string logTypeValue, string createdBy)
    {
        return new LogLibDm
        {
            ObjectId = Id,
            ObjectFirstVersionId = null,
            ObjectType = nameof(QuantityDatumLibDm),
            ObjectName = Name,
            ObjectVersion = null,
            LogType = logType,
            LogTypeValue = logTypeValue,
            CreatedBy = createdBy
        };
    }
}