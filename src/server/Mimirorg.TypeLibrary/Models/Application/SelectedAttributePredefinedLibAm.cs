using System.ComponentModel.DataAnnotations;
using Mimirorg.TypeLibrary.Enums;

namespace Mimirorg.TypeLibrary.Models.Application
{
    public class SelectedAttributePredefinedLibAm
    {
        [Required]
        public string Key { get; set; }
        
        [Required]
        public bool IsMultiSelect { get; set; }

        [Required]
        public virtual Dictionary<string, bool> Values { get; set; }

        [Required]
        public Aspect Aspect { get; set; }

        // TODO: Validate if not multi-select, only one value should be selected
    }
}
