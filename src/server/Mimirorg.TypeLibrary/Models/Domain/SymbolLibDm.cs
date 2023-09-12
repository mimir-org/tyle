using Mimirorg.Common.Contracts;
using Mimirorg.Common.Enums;
using Mimirorg.TypeLibrary.Enums;

namespace Mimirorg.TypeLibrary.Models.Domain;

public class SymbolLibDm : IStatefulObject //, ILogable
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Iri { get; set; }
    public string TypeReference { get; set; }
    public DateTime Created { get; set; }
    public string CreatedBy { get; set; }
    public State State { get; set; }
    public string Data { get; set; }
    /*public LogLibDm CreateLog(LogType logType, string logTypeValue, string createdBy)
    {
        return new LogLibDm
        {
            ObjectId = Id,
            ObjectFirstVersionId = null,
            ObjectType = nameof(SymbolLibDm),
            ObjectName = Name,
            ObjectVersion = null,
            LogType = logType,
            LogTypeValue = logTypeValue,
            Created = DateTime.UtcNow,
            CreatedBy = createdBy
        };
    }*/
}