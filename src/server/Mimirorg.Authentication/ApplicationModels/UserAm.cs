using System.ComponentModel.DataAnnotations;
using Mimirorg.Authentication.Models;

namespace Mimirorg.Authentication.ApplicationModels
{
    public class UserAm
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "{0} is required")]
        [EmailAddress(ErrorMessage = "{0} is not valid")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "{0} is required")]
        [MinLength(8, ErrorMessage = "{0} has min length {1}")]
        [Compare("ConfirmPassword", ErrorMessage = "The passwords are not equal")]
        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "{0} is required")]
        [MinLength(8, ErrorMessage = "{0} has min length {1}")]
        [Compare("Password", ErrorMessage = "The passwords are not equal")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Firstname")]
        [Required(ErrorMessage = "{0} is required")]
        public string FirstName { get; set; }

        [Display(Name = "Lastname")]
        [Required(ErrorMessage = "{0} is required")]
        public string LastName { get; set; }

        [Display(Name = "Phone Number")]
        [MaxLength(8, ErrorMessage = "{0} has max length {1}")]
        [RegularExpression("\\d*", ErrorMessage = "{0} must be numeric")]
        public string PhoneNumber { get; set; }

        public MimirorgUser ToMimirorgUser()
        {
            return new MimirorgUser
            {
                UserName = Email,
                Email = Email,
                FirstName = FirstName,
                LastName = LastName,
                PhoneNumber = PhoneNumber,
                TwoFactorEnabled = true
            };
        }

        //public ApplicationUser UpdateFromApplicationUserModel(MimirorgUser current)
        //{
        //    if (current == null)
        //        throw new ArgumentException("The model is not valid");

        //    current.UserName = Email;
        //    current.Email = Email;
        //    current.FirstName = FirstName;
        //    current.LastName = LastName;
        //    current.Address = Address;
        //    current.Address2 = Address2;
        //    current.ZipCode = ZipCode;
        //    current.ZipAddress = ZipAddress;
        //    current.Dob = Dob;
        //    current.PhoneNumber = PhoneNumber;
        //    current.TwoFactorEnabled = EnableTwoFactor;

        //    return current;
        //}
    }
}
