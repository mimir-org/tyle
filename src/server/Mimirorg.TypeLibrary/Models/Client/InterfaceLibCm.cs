using Mimirorg.Common.Enums;
using Mimirorg.TypeLibrary.Models.Data;

namespace Mimirorg.TypeLibrary.Models.Client
{
    public class InterfaceLibCm
    {
        public string Id { get; set; }
        public string ParentId { get; set; }
        public string Version { get; set; } = "1.0";
        public string Rds { get; set; }
        public string Category { get; set; }
        public Aspect Aspect { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string StatusId { get; set; } = "4590637F39B6BA6F39C74293BE9138DF";
        public string Iri { get; set; }
        public string TerminalId { get; set; }
        public ObjectType LibraryType => ObjectType.Interface;
        public PurposeLibDm Purpose { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
    }
}
