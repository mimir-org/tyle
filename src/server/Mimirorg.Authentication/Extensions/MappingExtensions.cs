using Mimirorg.Authentication.Models.Application;
using Mimirorg.Authentication.Models.Client;
using Mimirorg.Authentication.Models.Domain;

namespace Mimirorg.Authentication.Extensions;

public static class MappingExtensions
{
    public static MimirorgUser ToDomainModel(this UserRequest user)
    {
        return new MimirorgUser
        {
            UserName = user.Email,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Purpose = user.Purpose
        };
    }

    public static MimirorgUser UpdateDomainModel(this MimirorgUser self, UserRequest update)
    {
        self.FirstName = update.FirstName;
        self.LastName = update.LastName;
        self.Purpose = update.Purpose;
        return self;
    }

    public static UserView ToContentModel(this MimirorgUser user)
    {
        return new UserView
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Purpose = user.Purpose
        };
    }

    public static TokenView ToContentModel(this MimirorgToken token)
    {
        return new TokenView
        {
            ClientId = token.ClientId,
            Secret = token.Secret,
            TokenType = token.TokenType,
            ValidTo = token.ValidTo
        };
    }
}