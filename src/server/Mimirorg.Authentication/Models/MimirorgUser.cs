using Microsoft.AspNetCore.Identity;

namespace Mimirorg.Authentication.Models
{
    public class MimirorgUser : IdentityUser
    {
        #region Properties

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsLockedOut => CheckIfLockedOut();
        public bool ShouldBeLockedOut => CheckIfShouldBeLockedOut();

        #endregion

        #region Private methods

        private bool CheckIfShouldBeLockedOut()
        {
            // TODO: implement this
            return false;
        }

        private bool CheckIfLockedOut()
        {
            // TODO: implement this
            return false;
        }

        #endregion
    }
}
