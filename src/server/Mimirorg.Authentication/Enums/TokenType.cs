using System.ComponentModel.DataAnnotations;

namespace Mimirorg.Authentication.Enums;

public enum TokenType
{
    [Display(Name = "Access Token")]
    AccessToken = 0,

    [Display(Name = "Refresh Token")]
    RefreshToken = 1,

    [Display(Name = "Verify Email")]
    VerifyEmail = 2,

    [Display(Name = "Change password")]
    ChangePassword = 3,

    [Display(Name = "Change two factor")]
    ChangeTwoFactor = 4
}