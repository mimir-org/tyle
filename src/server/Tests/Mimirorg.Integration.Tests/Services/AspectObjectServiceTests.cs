using Microsoft.Extensions.DependencyInjection;
using Mimirorg.Common.Enums;
using Mimirorg.Test.Setup;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using TypeLibrary.Services.Contracts;
using Xunit;

namespace Mimirorg.Test.Integration.Services;

public class AspectObjectServiceTests : IntegrationTest
{
    public AspectObjectServiceTests(ApiWebApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Create_AspectObject_Create_AspectObject_When_Ok_Parameters()
    {
        var aspectObjectService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<IAspectObjectService>();
        var terminalService =
            Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<ITerminalService>();
        var logService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<ILogService>();

        var terminal = terminalService.Get().ToList()[0];

        var aspectObjectAm = new AspectObjectLibAm
        {
            Name = "AspectObject2",
            RdsId = "rds-id",
            PurposeName = "PurposeName",
            Description = "Description",
            Aspect = Aspect.NotSet,
            CompanyId = 1,
            AspectObjectTerminals = new List<AspectObjectTerminalLibAm>{
                new()
                {
                    TerminalId = terminal.Id,
                    MinQuantity = 1,
                    MaxQuantity = int.MaxValue,
                    ConnectorDirection = ConnectorDirection.Output
                }
            },
            SelectedAttributePredefined = new List<SelectedAttributePredefinedLibAm>{
                new()
                {
                    Key = "1234",
                    IsMultiSelect = true,
                    Values = new Dictionary<string, bool>
                    {
                        {"56789", true}
                    },
                    TypeReference = "https://url.com/1234567890"
                }
            },
            Symbol = "symbol",
            TypeReference = "https://url.com/1234567890",
            Version = "1.0"
        };

        var aspectObjectCm = await aspectObjectService.Create(aspectObjectAm);

        Assert.NotNull(aspectObjectCm);
        Assert.True(aspectObjectCm.State == State.Draft);
        Assert.Equal(aspectObjectAm.Name, aspectObjectCm.Name);
        Assert.Equal(aspectObjectAm.RdsId, aspectObjectCm.RdsId);
        Assert.Equal(aspectObjectAm.PurposeName, aspectObjectCm.PurposeName);
        Assert.Equal(aspectObjectAm.Aspect, aspectObjectCm.Aspect);
        Assert.Equal(aspectObjectAm.Description, aspectObjectCm.Description);
        Assert.Equal(aspectObjectAm.CompanyId, aspectObjectCm.CompanyId);

        foreach (var am in aspectObjectAm.AspectObjectTerminals)
        {
            foreach (var cm in aspectObjectCm.AspectObjectTerminals)
            {
                Assert.Equal(am.TerminalId, cm.Terminal.Id);
                Assert.Equal(am.MinQuantity, cm.MinQuantity);
                Assert.Equal(am.MaxQuantity, cm.MaxQuantity);
                Assert.Equal(am.ConnectorDirection.ToString(), cm.ConnectorDirection.ToString());
            }
        }

        Assert.Equal(aspectObjectAm.SelectedAttributePredefined.First().Key, aspectObjectCm.SelectedAttributePredefined.First().Key);
        Assert.Equal(aspectObjectAm.SelectedAttributePredefined.First().IsMultiSelect, aspectObjectCm.SelectedAttributePredefined.First().IsMultiSelect);
        Assert.Equal(aspectObjectAm.SelectedAttributePredefined.First().Values.ToString(), aspectObjectCm.SelectedAttributePredefined.First().Values.ToString());

        Assert.Equal(aspectObjectAm.SelectedAttributePredefined.First().TypeReference, aspectObjectCm.SelectedAttributePredefined.First().TypeReference);

        Assert.Equal(aspectObjectAm.TypeReference, aspectObjectCm.TypeReference);

        Assert.Equal(aspectObjectAm.Symbol, aspectObjectCm.Symbol);

        var logCm = logService.Get().FirstOrDefault(x => x.ObjectId == aspectObjectCm.Id && x.ObjectType == "AspectObjectLibDm");

        Assert.True(logCm != null);
        Assert.Equal(aspectObjectCm.Id, logCm.ObjectId);
        Assert.Equal(aspectObjectCm.FirstVersionId, logCm.ObjectFirstVersionId);
        Assert.Equal(aspectObjectCm.Name, logCm.ObjectName);
        Assert.Equal(aspectObjectCm.Version, logCm.ObjectVersion);
        Assert.Equal(aspectObjectCm.GetType().Name.Remove(aspectObjectCm.GetType().Name.Length - 2, 2) + "Dm", logCm.ObjectType);
        Assert.Equal(LogType.Create.ToString(), logCm.LogType.ToString());
        Assert.Equal(State.Draft.ToString(), logCm.LogTypeValue);
        Assert.NotNull(logCm.CreatedBy);
        Assert.Equal("System.DateTime", logCm.Created.GetType().ToString());
        Assert.True(logCm.Created.Kind == DateTimeKind.Utc);
    }

    [Fact]
    public async Task GetLatestVersions_AspectObjects_Result_Ok()
    {
        var aspectObjectAm = new AspectObjectLibAm
        {
            Name = "AspectObject4",
            RdsId = "rds-id",
            PurposeName = "PurposeName",
            Description = "Description",
            Aspect = Aspect.NotSet,
            CompanyId = 1,
            Version = "1.0"
        };

        var aspectObjectService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<IAspectObjectService>();
        var aspectObjectCm = await aspectObjectService.Create(aspectObjectAm);

        aspectObjectAm.Description = "Description v1.1";

        var aspectObjectCmUpdated = await aspectObjectService.Update(aspectObjectCm.Id, aspectObjectAm);

        Assert.True(aspectObjectCm?.Description == "Description");
        Assert.True(aspectObjectCm.Version == "1.0");
        Assert.True(aspectObjectCmUpdated?.Description == "Description v1.1");
        Assert.True(aspectObjectCmUpdated.Version == "1.1");
    }


    [Fact]
    public async Task Update_AspectObject_Result_Ok()
    {
        var aspectObjectAm = new AspectObjectLibAm
        {
            Name = "AspectObject6",
            RdsId = "rds-id",
            PurposeName = "PurposeName",
            Description = "Description1",
            Aspect = Aspect.NotSet,
            CompanyId = 1,
            Version = "1.0"
        };

        var aspectObjectService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<IAspectObjectService>();

        var cm = await aspectObjectService.Create(aspectObjectAm);
        aspectObjectAm.Description = "Description2";
        var cmUpdated = await aspectObjectService.Update(cm.Id, aspectObjectAm);

        Assert.True(cm.Description == "Description1" && cm.Version == "1.0");
        Assert.True(cmUpdated.Description == "Description2" && cmUpdated.Version == "1.1");
    }
}