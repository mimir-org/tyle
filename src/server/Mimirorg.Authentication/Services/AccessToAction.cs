using System.Security.Claims;
using Tyle.Core.Common;

namespace Mimirorg.Authentication.Services;

    public static class AccessToAction
    {
        public static bool HasUserPermissionToModify(ClaimsPrincipal? user, string createdNameFromDb, State stateFromDb)
        {
            if (user == null && stateFromDb == State.Approved)
                return false;

            if (user.IsInRole("Administrator") || user.IsInRole("Reviewer"))
                return true;

            if (user.IsInRole("Contributer") && createdNameFromDb == user.Identity.Name)
                return true;

            return false;

        }


    }



