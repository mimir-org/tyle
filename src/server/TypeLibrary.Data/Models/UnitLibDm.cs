using Mimirorg.Common.Enums;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using System;
using System.Collections.Generic;
using Mimirorg.TypeLibrary.Extensions;
using TypeLibrary.Data.Contracts.Common;

namespace TypeLibrary.Data.Models;

public class UnitLibDm : ILogable, IEquatable<UnitLibDm>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Iri { get; set; }
    public string TypeReference { get; set; }
    public string Symbol { get; set; }
    public State State { get; set; }
    public int? CompanyId { get; set; }
    public string Description { get; set; }
    public DateTime Created { get; set; }
    public string CreatedBy { get; set; }
    public ICollection<AttributeUnitLibDm> UnitAttributes { get; set; }

    public LogLibAm CreateLog(LogType logType, string logTypeValue, string comment)
    {
        return new LogLibAm
        {
            ObjectId = Id,
            ObjectFirstVersionId = Id,
            ObjectType = nameof(UnitLibDm),
            ObjectName = Name,
            ObjectVersion = "",
            LogType = logType,
            LogTypeValue = logTypeValue,
            Comment = comment
        };
    }

    public bool Equals(UnitLibDm other)
    {
        if (other == null) return false;
        return this.Name == other.Name && this.Symbol == other.Symbol;
    }
}