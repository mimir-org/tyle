using System.ComponentModel.DataAnnotations;

namespace Mimirorg.TypeLibrary.Enums
{
    public enum MimirorgTokenType
    {
        [Display(Name = "Access Token")]
        AccessToken = 0,

        [Display(Name = "Refresh Token")]
        RefreshToken = 1,

        [Display(Name = "Verify Email")]
        VerifyEmail = 2,

        [Display(Name = "Verify Phone")]
        VerifyPhone = 3,

        [Display(Name = "Change password")]
        ChangePassword = 4,

        [Display(Name = "Change two factor")]
        ChangeTwoFactor = 5
    }
}