using Mimirorg.TypeLibrary.Enums;

namespace Mimirorg.TypeLibrary.Models.Client
{
    public class LogLibCm
    {
        public string Id { get; set; }
        public int ObjectId { get; set; }
        public int ObjectFirstVersionId { get; set; }
        public string ObjectName { get; set; }
        public string ObjectVersion { get; set; }
        public string ObjectType { get; set; }
        public LogType LogType { get; set; }
        public string LogTypeValue { get; set; }
        public string Comment { get; set; }
        public string User { get; set; }
        public DateTime Created { get; set; }
        public string Kind => nameof(LogLibCm);
    }
}