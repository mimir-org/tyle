using System;
using System.ComponentModel.DataAnnotations;

namespace TypeLibrary.Models.Enums
{
    [Flags]
    public enum Discipline
    {
        [Display(Name = "None")]
        None = 0,

        [Display(Name = "Not set")]
        NotSet = 1,

        [Display(Name = "Project management and administration")]
        ProjectManagement = 2,

        [Display(Name = "Electrical")]
        Electrical = 4,

        [Display(Name = "Automation")]
        Automation = 8,

        [Display(Name = "Structural")]
        Structural = 16,

        [Display(Name = "Operation")]
        Operation = 32,

        [Display(Name = "Process")]
        Process = 64
    }
}