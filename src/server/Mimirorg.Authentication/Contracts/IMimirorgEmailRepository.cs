using Mimirorg.TypeLibrary.Models.Application;

namespace Mimirorg.Authentication.Contracts;

public interface IMimirorgEmailRepository
{
    Task SendEmail(MimirorgMailAm email);
}