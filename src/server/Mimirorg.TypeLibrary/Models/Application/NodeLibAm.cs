using System.ComponentModel.DataAnnotations;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Extensions;

namespace Mimirorg.TypeLibrary.Models.Application
{
    //public interface IVersionObject<in T> where T : class, new()
    //{
    //    bool HasNewVersion(T other);
    //}

    public class NodeLibAm// : IVersionObject<NodeLibAm>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string RdsName { get; set; }

        [Required]
        public string RdsCode { get; set; }

        [Required]
        public string PurposeName { get; set; } //minor

        [Required]
        public Aspect Aspect { get; set; }

        [Required]
        public int CompanyId { get; set; } //minor

        public ICollection<string> SimpleIdList { get; set; } //fjerning = exception, legg til = major
        public ICollection<string> AttributeIdList { get; set; } //fjerning = exception, legg til = major
        public ICollection<NodeTerminalLibAm> NodeTerminals { get; set; } //fjerning = exception, legg til = major
        public ICollection<SelectedAttributePredefinedLibAm> SelectedAttributePredefined { get; set; } //fjerning = exception, legg til = major

        public string Description { get; set; } //minor
        public string Symbol { get; set; } //minor
        public string AttributeAspectIri { get; set; } //minor
        public string Version { get; set; } = "1.0";
        public string FirstVersionId { get; set; }
        public ICollection<string> ContentReferences { get; set; } //minor
        public string ParentId { get; set; } //exception

        public string Id => $"{Name}-{RdsCode}-{Aspect}-{Version}".CreateMd5();
    }
}