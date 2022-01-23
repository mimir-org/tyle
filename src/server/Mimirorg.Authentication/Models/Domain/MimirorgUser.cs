using Microsoft.AspNetCore.Identity;
using Mimirorg.Authentication.Models.Application;
using Mimirorg.Authentication.Models.Content;

namespace Mimirorg.Authentication.Models.Domain
{
    public class MimirorgUser : IdentityUser
    {
        #region Properties

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SecurityHash { get; set; }
        public List<MimirorgCompany> MangeCompanies { get; set; }
        
        public MimirorgUserCm ToContentModel()
        {
            return new MimirorgUserCm
            {
                Id = Id,
                Email = Email,
                FirstName = FirstName,
                LastName = LastName,
                PhoneNumber = PhoneNumber
            };
        }

        public void FromApplicationModel(MimirorgUserAm user)
        {
            Email = user.Email;
            FirstName = user.FirstName;
            LastName = user.LastName;
            PhoneNumber = user.PhoneNumber;
        }

        #endregion
    }
}
