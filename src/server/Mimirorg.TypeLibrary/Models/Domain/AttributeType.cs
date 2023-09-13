using Mimirorg.TypeLibrary.Enums;

namespace Mimirorg.TypeLibrary.Models.Domain;

public class AttributeType : ImfType // ILogable, IStatefulObject
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