namespace Tyle.Application.Common;

public interface IUserInformationService
{
    string GetUserId();

    Task<string?> GetFullName(string userId);

    Task<string?> GetEmail(string userId);
}