using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Mimirorg.Authentication.Models;
using Moq;
using TypeLibrary.Data;

namespace Mimirorg.Test.Setup.Fixtures;

public class MimirorgCommonFixture : IDisposable
{
    // Common
    public MimirorgAuthSettings MimirorgAuthSettings = new();
    public ApplicationSettings ApplicationSettings = new();
    public Mock<IMapper> Mapper = new();
    public Mock<IHttpContextAccessor> HttpContextAccessor = new();
    public TyleDbContext TyleContext { get; set; } = new TyleDbContext(new DbContextOptionsBuilder<TyleDbContext>()
        .UseInMemoryDatabase($"TestDb{DateTime.Now}").Options);

    public MimirorgCommonFixture()
    {
        ApplicationSettings.ApplicationSemanticUrl = @"http://localhost:5001/v1/ont";
        ApplicationSettings.ApplicationUrl = @"http://localhost:5001";
        MimirorgAuthSettings.ApplicationUrl = @"http://localhost:5001";
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