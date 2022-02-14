using System.ComponentModel.DataAnnotations;

namespace Mimirorg.TypeLibrary.Models.Application
{
    public class AttributePredefinedLibAm
    {
        [Required]
        public string Key { get; set; }
        public bool IsMultiSelect { get; set; }
        public ICollection<string> ValueStringList { get; set; }
        // TODO: Skulle hatt kobling til Aspect
    }
}
