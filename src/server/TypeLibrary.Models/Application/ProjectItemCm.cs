using System;
using TypeLibrary.Models.Extensions;

namespace TypeLibrary.Models.Application
{
    public class ProjectItemCm
    {
        public string Id { get; set; }
        public string Iri { get; set; }
        public string Domain => Id.ResolveDomain();
        public string Name { get; set; }
        public string Version { get; set; }
        public string Description { get; set; }
        public string ProjectOwner { get; set; }
        public DateTime Updated { get; set; }
        public string UpdatedBy { get; set; }
    }
}
