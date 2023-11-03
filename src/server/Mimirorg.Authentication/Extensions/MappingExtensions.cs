using Mimirorg.Authentication.Models.Application;
using Mimirorg.Authentication.Models.Client;
using Mimirorg.Authentication.Models.Domain;

namespace Mimirorg.Authentication.Extensions;

public static class MappingExtensions
{
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
            Purpose = user.Purpose
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