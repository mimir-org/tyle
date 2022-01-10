using System;
using System.ComponentModel.DataAnnotations;

namespace TypeLibrary.Models.Enums
{
    [Flags]
    public enum CommitStatus
    {
        [Display(Name = "Not set")]
        NotSet = 0,

        [Display(Name = "Working")]
        Working = 1,

        [Display(Name = "Review")]
        Review = 2,

        [Display(Name = "Approved")]
        Approved = 4,

        [Display(Name = "Committed")]
        Committed = 8,

        [Display(Name = "Sent")]
        Sent = 16
    }
}
