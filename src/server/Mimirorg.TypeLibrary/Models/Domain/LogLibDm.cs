using Mimirorg.TypeLibrary.Enums;

namespace Mimirorg.TypeLibrary.Models.Domain;

public class LogLibDm
{
    public int Id { get; set; }
    public string ObjectId { get; set; }
    public string ObjectFirstVersionId { get; set; }
    public string ObjectVersion { get; set; }
    public string ObjectType { get; set; }
    public string ObjectName { get; set; }
    public DateTime Created { get; set; }
    public string CreatedBy { get; set; }
    public LogType LogType { get; set; }
    public string LogTypeValue { get; set; }
}