using Microsoft.EntityFrameworkCore;
using Mimirorg.Authentication.Models;
using Tyle.Persistence;

namespace Tyle.Test.Setup.Fixtures;

public class MimirorgCommonFixture : IDisposable
{
    // Common
    public MimirorgAuthSettings MimirorgAuthSettings = new();
    public TyleDbContext TyleContext { get; } = new(new DbContextOptionsBuilder<TyleDbContext>().UseInMemoryDatabase($"TestDb{DateTime.Now}").Options);

    public MimirorgCommonFixture()
    {
        MimirorgAuthSettings.RequireDigit = true;
        MimirorgAuthSettings.RequireNonAlphanumeric = true;
        MimirorgAuthSettings.RequireUppercase = true;
        MimirorgAuthSettings.RequiredLength = 10;
    }

    public void Dispose()
    {
        TyleContext.Dispose();
    }
}