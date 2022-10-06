using Mimirorg.Authentication.Models.Domain;
using Mimirorg.Common.Extensions;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;

namespace Mimirorg.Authentication.Extensions
{
    public static class MappingExtensions
    {
        public static MimirorgCompany ToDomainModel(this MimirorgCompanyAm company)
        {
            return new MimirorgCompany
            {
                Name = company.Name,
                DisplayName = company.DisplayName,
                Description = company.Description,
                ManagerId = company.ManagerId,
                Secret = company.Secret,
                Domain = company.Domain,
                Logo = company.Logo,
                HomePage = company.HomePage,
                Iris = company.Iris?.ConvertToString()
            };
        }

        public static MimirorgCompanyCm ToContentModel(this MimirorgCompany company)
        {
            return new MimirorgCompanyCm
            {
                Id = company.Id,
                Name = company.Name,
                DisplayName = company.DisplayName,
                Description = company.Description,
                Manager = company.Manager?.ToContentModel(),
                Secret = company.Secret,
                Domain = company.Domain,
                Logo = company.Logo,
                HomePage = company.HomePage,
                Iris = company.Iris?.ConvertToArray()
            };
        }

        public static MimirorgHook ToDomainModel(this MimirorgHookAm hook)
        {
            return new MimirorgHook
            {
                CompanyId = hook.CompanyId,
                Key = hook.Key,
                Iri = hook.Iri
            };
        }

        public static MimirorgHookCm ToContentModel(this MimirorgHook hook)
        {
            return new MimirorgHookCm
            {
                Id = hook.Id,
                CompanyId = hook.CompanyId,
                Company = hook.Company.ToContentModel(),
                Key = hook.Key,
                Iri = hook.Iri
            };
        }

        public static MimirorgUser ToDomainModel(this MimirorgUserAm user)
        {
            return new MimirorgUser
            {
                UserName = user.Email,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                CompanyId = user.CompanyId,
                Purpose = user.Purpose
            };
        }

        public static MimirorgUser UpdateDomainModel(this MimirorgUser self, MimirorgUserAm update)
        {
            self.FirstName = update.FirstName;
            self.LastName = update.LastName;
            self.CompanyId = update.CompanyId;
            self.Purpose = update.Purpose;
            return self;
        }

        public static MimirorgUserCm ToContentModel(this MimirorgUser user)
        {
            return new MimirorgUserCm
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                CompanyId = user.CompanyId,
                CompanyName = user.CompanyName,
                Purpose = user.Purpose,
                EmailConfirmed = user.EmailConfirmed
            };
        }

        public static MimirorgTokenCm ToContentModel(this MimirorgToken token)
        {
            return new MimirorgTokenCm
            {
                ClientId = token.ClientId,
                Secret = token.Secret,
                TokenType = token.TokenType,
                ValidTo = token.ValidTo
            };
        }
    }
}