using System.ComponentModel.DataAnnotations;

namespace TypeLibrary.Models.Enums
{
    public enum ModuleType
    {
        [Display(Name = "Plugin")]
        Plugin = 0,

        [Display(Name = "Parser")]
        Parser = 1,

        [Display(Name = "SyncService")]
        SyncService = 2
    }
}
