using Mimirorg.Authentication.Contracts;
using Mimirorg.TypeLibrary.Models.Application;

namespace Mimirorg.Authentication.Repositories;

public class SendLocalRepository : IMimirorgEmailRepository
{
    public async Task SendEmail(MimirorgMailAm email)
    {
        if (string.IsNullOrWhiteSpace(email?.FromEmail) || string.IsNullOrWhiteSpace(email.ToEmail))
            return;

        var assemblyLocation = System.Reflection.Assembly.GetExecutingAssembly().Location;
        var filePath = $@"{Path.GetDirectoryName(assemblyLocation)}/Data/Mail/{Guid.NewGuid()}.txt";
        var file = new FileInfo(filePath);
        file.Directory?.Create();
        await File.WriteAllTextAsync(file.FullName, email.ToString());
    }
}