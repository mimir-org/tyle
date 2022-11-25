using System.Security.Claims;
using Mimirorg.TypeLibrary.Enums;

namespace Mimirorg.TypeLibrary.Models.Client
{
    public class MimirorgUserCm
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Dictionary<int, MimirorgPermission> Permissions { get; } = new();
        public ICollection<string> Roles { get; } = new List<string>();
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string Purpose { get; set; }

        /// <summary>
        /// Resolves the role names for the user
        /// </summary>
        /// <param name="roles">A list of roles for the user</param>
        /// <param name="claims">A list of claim for the user</param>
        /// <param name="companies">A collection of all the registered companies</param>
        /// <param name="permissions">A collection of all permissions</param>
        /// <returns>A collection of role names</returns>
        public void ResolveRoles(IEnumerable<string> roles, IEnumerable<Claim> claims, ICollection<MimirorgCompanyCm> companies, ICollection<MimirorgPermissionCm> permissions)
        {
            foreach (var role in roles)
            {
                switch (role)
                {
                    case "Administrator":
                        Roles.Add("Global administrator");
                        break;
                    case "Account Manager":
                        Roles.Add("Global account manager");
                        break;
                    case "Moderator":
                        Roles.Add("Global moderator");
                        break;
                }
            }

            if (Roles.Any())
            {
                return;
            }


            if (!companies.Any())
            {
                return;
            }

            var userCompanyClaims = claims.Where(x => companies.Any(y => x.Type == y.Id.ToString())).ToList();
            if (!userCompanyClaims.Any())
            {
                return;
            }

            foreach (var claim in userCompanyClaims)
            {
                var company = companies.FirstOrDefault(x => x.Id.ToString() == claim.Type);
                var permission = permissions.FirstOrDefault(x => x.Name == claim.Value);
                if (company != null && permission != null)
                {
                    Roles.Add($"{company.DisplayName ?? company.Name} {(MimirorgPermission) permission.Id}");
                }
            }
        }

        /// <summary>
        /// Resolves permissions for the user
        /// </summary>
        /// <param name="roles">A list of roles for the user</param>
        /// <param name="claims">A list of claim for the user</param>
        /// <param name="companies">A collection of all the registered companies</param>
        /// <param name="permissions">A collection of all permissions</param>
        /// <returns>A collection of permission names</returns>
        public void ResolvePermissions(IList<string> roles, IList<Claim> claims, ICollection<MimirorgCompanyCm> companies, ICollection<MimirorgPermissionCm> permissions)
        {
            if (!companies.Any())
            {
                return;
            }

            // Administrator or Account Manager role should give full permission to all companies
            if (roles.Any(x => x is "Administrator" or "Account Manager"))
            {
                foreach (var company in companies)
                {
                    Permissions.Add(company.Id, MimirorgPermission.Manage);
                }

                return;
            }

            // Moderator role should give delete permission to all companies
            if (roles.Any(x => x is "Moderator"))
            {
                foreach (var company in companies)
                {
                    Permissions.Add(company.Id, MimirorgPermission.Delete);
                }

                return;
            }

            var userCompanyClaims = claims.Where(x => companies.Any(y => x.Type == y.Id.ToString())).ToList();
            foreach (var claim in userCompanyClaims)
            {
                var company = companies.FirstOrDefault(x => x.Id.ToString() == claim.Type);
                var permission = permissions.FirstOrDefault(x => x.Name == claim.Value);
                if (company != null && permission != null && Permissions.All(x => x.Key != company.Id))
                    Permissions.Add(company.Id, (MimirorgPermission) permission.Id);
            }
        }
    }
}