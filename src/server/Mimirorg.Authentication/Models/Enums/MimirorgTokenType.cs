using System.ComponentModel.DataAnnotations;

namespace Mimirorg.Authentication.Models.Enums
{
    public enum MimirorgTokenType
    {
        [Display(Name = "Access-Token")]
        AccessToken = 0,

        [Display(Name = "Refresh-Token")]
        RefreshToken = 1
    }
}
