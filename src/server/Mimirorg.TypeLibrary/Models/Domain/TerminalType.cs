using Mimirorg.TypeLibrary.Enums;

namespace Mimirorg.TypeLibrary.Models.Domain;

public class TerminalType : ImfType // : ILogable, IStatefulObject
{
    //public int CompanyId { get; set; }
    //public State State { get; set; }
    public ICollection<TerminalClassifierMapping> Classifiers { get; set; }
    public int? PurposeId { get; set; }
    public PurposeReference? Purpose { get; set; }
    public string? Notation { get; set; }
    public string? Symbol { get; set; }
    public Aspect? Aspect { get; set; }
    public int? MediumId { get; set; }
    public MediumReference? Medium { get; set; }
    public Direction Qualifier { get; set; }
    public ICollection<TerminalAttributeTypeReference> TerminalAttributes { get; set; }

    public TerminalType(string name, string? description, string createdBy) : base(name, description, createdBy)
    {
        Classifiers = new List<TerminalClassifierMapping>();
        TerminalAttributes = new List<TerminalAttributeTypeReference>();
    }

    /*#region ILogable

    public LogLibDm CreateLog(LogType logType, string logTypeValue, string createdBy)
    {
        return new LogLibDm
        {
            ObjectId = Id,
            ObjectFirstVersionId = null,
            ObjectType = nameof(TerminalType),
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