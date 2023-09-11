using Microsoft.AspNetCore.Http;
using Mimirorg.Common.Contracts;
using Mimirorg.Common.Enums;
using Mimirorg.TypeLibrary.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TypeLibrary.Data.Contracts.Common;

namespace TypeLibrary.Data.Models;

public class AttributeType : GenericType // ILogable, IStatefulObject
{
    //public int CompanyId { get; set; }
    //public State State { get; set; }
    public int? PredicateId { get; set; }
    public PredicateReference? Predicate { get; set; }
    public ICollection<AttributeUnitMapping> Units { get; }
    public int UnitMinCount { get; set; }
    public int UnitMaxCount { get; set; }
    public ProvenanceQualifier? ProvenanceQualifier { get; set; }
    public RangeQualifier? RangeQualifier { get; set; }
    public RegularityQualifier? RegularityQualifier { get; set; }
    public ScopeQualifier? ScopeQualifier { get; set; }
    public ValueConstraint? ValueConstraint { get; set; }

    public AttributeType(string name, string? description, string createdBy) : base(name, description, createdBy)
    {
        Units = new List<AttributeUnitMapping>();
    }

    /*public LogLibDm CreateLog(LogType logType, string logTypeValue, string createdBy)
    {
        return new LogLibDm
        {
            ObjectId = Id,
            ObjectFirstVersionId = null,
            ObjectType = nameof(AttributeType),
            ObjectName = Name,
            ObjectVersion = null,
            LogType = logType,
            LogTypeValue = logTypeValue,
            Created = DateTime.UtcNow,
            CreatedBy = createdBy
        };
    }*/
}