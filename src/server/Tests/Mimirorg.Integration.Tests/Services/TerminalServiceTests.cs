using Microsoft.Extensions.DependencyInjection;
using Mimirorg.Common.Enums;
using Mimirorg.Test.Setup;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using TypeLibrary.Services.Contracts;
using Xunit;

namespace Mimirorg.Test.Integration.Services;

public class TerminalServiceTests : IntegrationTest
{
    public TerminalServiceTests(ApiWebApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Create_Terminal_Create_Terminal_When_Ok_Parameters()
    {
        var terminalAm = new TerminalLibAm
        {
            Name = "TestTerminal2",
            ParentId = "1234",
            TypeReference = "https://url.com/1234567890",
            Color = "#123456",
            Description = "Description1"
        };

        var terminalService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<ITerminalService>();
        var logService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<ILogService>();

        var terminalCm = await terminalService.Create(terminalAm);

        Assert.NotNull(terminalCm);
        Assert.True(terminalCm.State == State.Draft);
        Assert.Equal(terminalAm.ParentId, terminalCm.ParentId);

        Assert.Equal(terminalAm.TypeReference, terminalCm.TypeReference);

        Assert.Equal(terminalAm.Color, terminalCm.Color);
        Assert.Equal(terminalAm.Description, terminalCm.Description);

        var logCm = logService.Get().FirstOrDefault(x => x.ObjectId == terminalCm.Id && x.ObjectType == "TerminalLibDm");

        Assert.True(logCm != null);
        Assert.Equal(terminalCm.Id, logCm.ObjectId);
        Assert.Equal(terminalCm.Name, logCm.ObjectName);
        Assert.Equal(terminalCm.GetType().Name.Remove(terminalCm.GetType().Name.Length - 2, 2) + "Dm", logCm.ObjectType);
        Assert.Equal(LogType.State.ToString(), logCm.LogType.ToString());
        Assert.Equal(State.Draft.ToString(), logCm.LogTypeValue);
        Assert.NotNull(logCm.User);
        Assert.Equal("System.DateTime", logCm.Created.GetType().ToString());
        Assert.True(logCm.Created.Kind == DateTimeKind.Utc);
    }

    [Fact]
    public async Task GetLatestVersions_Terminal_Result_Ok()
    {
        var terminalAm = new TerminalLibAm
        {
            Name = "TestTerminal3",
            ParentId = "1234",
            TypeReference = null,
            Color = "#123456",
            Description = "Description v1.0"
        };

        var terminalService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<ITerminalService>();
        var terminalLibCm = await terminalService.Create(terminalAm);

        terminalAm.Description = "Description v1.1";

        var terminalCmUpdated = await terminalService.Update(terminalLibCm.Id, terminalAm);

        Assert.True(terminalLibCm?.Description == "Description v1.0");
        Assert.True(terminalCmUpdated?.Description == "Description v1.1");
    }
}