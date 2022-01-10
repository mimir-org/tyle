using TypeLibrary.Models.Enums;

namespace TypeLibrary.Models.Application
{
    public class ImfData
    {
        public string Id => $"{ProjectId}_{Version}";
        public string ProjectId { get; set; }
        public string Version { get; set; }
        public string Environment { get; set; }
        public string Parser { get; set; }
        public string SenderDomain { get; set; }
        public string ReceivingDomain { get; set; }
        public string Document { get; set; }
        public CommitStatus CommitStatus { get; set; }
    }
}
