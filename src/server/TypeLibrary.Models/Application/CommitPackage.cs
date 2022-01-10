using System.ComponentModel.DataAnnotations;
using TypeLibrary.Models.Enums;

namespace TypeLibrary.Models.Application
{
    public class CommitPackage
    {
        [Required]
        public string ProjectId { get; set; }

        [EnumDataType(typeof(CommitStatus))]
        public CommitStatus CommitStatus { get; set; }

        [Required]
        public string Parser { get; set; }

        [Required]
        public string ReceivingDomain { get; set; }
    }
}
