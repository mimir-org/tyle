/*using Microsoft.Extensions.DependencyInjection;
using Mimirorg.Common.Enums;
using Mimirorg.Test.Setup;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using TypeLibrary.Services.Contracts;
using Xunit;

namespace Mimirorg.Test.Integration.Services;

public class BlockServiceTests : IntegrationTest
{
    public BlockServiceTests(ApiWebApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Create_Block_Create_Block_When_Ok_Parameters()
    {
        var blockService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<IBlockService>();
        var terminalService =
            Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<ITerminalService>();
        var logService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<ILogService>();

        var terminal = terminalService.Get().ToList()[0];

        var blockAm = new BlockLibAm
        {
            Name = "Block2",
            RdsId = "rds-id",
            PurposeName = "PurposeName",
            Description = "Description",
            Aspect = Aspect.NotSet,
            CompanyId = 1,
            BlockTerminals = new List<BlockTerminalLibAm>{
                new()
                {
                    TerminalId = terminal.Id,
                    MinQuantity = 1,
                    MaxQuantity = int.MaxValue,
                    Direction = Direction.Output
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

        var blockCm = await blockService.Create(blockAm);

        Assert.NotNull(blockCm);
        Assert.True(blockCm.State == State.Draft);
        Assert.Equal(blockAm.Name, blockCm.Name);
        Assert.Equal(blockAm.RdsId, blockCm.RdsId);
        Assert.Equal(blockAm.PurposeName, blockCm.PurposeName);
        Assert.Equal(blockAm.Aspect, blockCm.Aspect);
        Assert.Equal(blockAm.Description, blockCm.Description);
        Assert.Equal(blockAm.CompanyId, blockCm.CompanyId);

        foreach (var am in blockAm.BlockTerminals)
        {
            foreach (var cm in blockCm.BlockTerminals)
            {
                Assert.Equal(am.TerminalId, cm.Terminal.Id);
                Assert.Equal(am.MinQuantity, cm.MinQuantity);
                Assert.Equal(am.MaxQuantity, cm.MaxQuantity);
                Assert.Equal(am.Direction.ToString(), cm.Direction.ToString());
            }
        }

        Assert.Equal(blockAm.SelectedAttributePredefined.First().Key, blockCm.SelectedAttributePredefined.First().Key);
        Assert.Equal(blockAm.SelectedAttributePredefined.First().IsMultiSelect, blockCm.SelectedAttributePredefined.First().IsMultiSelect);
        Assert.Equal(blockAm.SelectedAttributePredefined.First().Values.ToString(), blockCm.SelectedAttributePredefined.First().Values.ToString());

        Assert.Equal(blockAm.SelectedAttributePredefined.First().TypeReference, blockCm.SelectedAttributePredefined.First().TypeReference);

        Assert.Equal(blockAm.TypeReference, blockCm.TypeReference);

        Assert.Equal(blockAm.Symbol, blockCm.Symbol);

        var logCm = logService.Get().FirstOrDefault(x => x.ObjectId == blockCm.Id && x.ObjectType == "BlockLibDm");

        Assert.True(logCm != null);
        Assert.Equal(blockCm.Id, logCm.ObjectId);
        Assert.Equal(blockCm.FirstVersionId, logCm.ObjectFirstVersionId);
        Assert.Equal(blockCm.Name, logCm.ObjectName);
        Assert.Equal(blockCm.Version, logCm.ObjectVersion);
        Assert.Equal(blockCm.GetType().Name.Remove(blockCm.GetType().Name.Length - 2, 2) + "Dm", logCm.ObjectType);
        Assert.Equal(LogType.Create.ToString(), logCm.LogType.ToString());
        Assert.Equal(State.Draft.ToString(), logCm.LogTypeValue);
        Assert.NotNull(logCm.CreatedBy);
        Assert.Equal("System.DateTime", logCm.Created.GetType().ToString());
        Assert.True(logCm.Created.Kind == DateTimeKind.Utc);
    }

    [Fact]
    public async Task GetLatestVersions_Blocks_Result_Ok()
    {
        var blockAm = new BlockLibAm
        {
            Name = "Block4",
            RdsId = "rds-id",
            PurposeName = "PurposeName",
            Description = "Description",
            Aspect = Aspect.NotSet,
            CompanyId = 1,
            Version = "1.0"
        };

        var blockService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<IBlockService>();
        var blockCm = await blockService.Create(blockAm);
        await blockService.ChangeState(blockCm.Id, State.Approved, false);

        blockAm.Description = "Description v1.1";

        var blockCmUpdated = await blockService.Update(blockCm.Id, blockAm);

        Assert.True(blockCm.Description == "Description");
        Assert.True(blockCm.Version == "1.0");
        Assert.True(blockCmUpdated?.Description == "Description v1.1");
        Assert.True(blockCmUpdated.Version == "1.1");
    }


    [Fact]
    public async Task Update_Block_Result_Ok()
    {
        var blockAm = new BlockLibAm
        {
            Name = "Block6",
            RdsId = "rds-id",
            PurposeName = "PurposeName",
            Description = "Description1",
            Aspect = Aspect.NotSet,
            CompanyId = 1,
            Version = "1.0"
        };

        var blockService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<IBlockService>();

        var cm = await blockService.Create(blockAm);
        await blockService.ChangeState(cm.Id, State.Approved, false);
        blockAm.Description = "Description2";
        var cmUpdated = await blockService.Update(cm.Id, blockAm);

        Assert.True(cm.Description == "Description1" && cm.Version == "1.0");
        Assert.True(cmUpdated.Description == "Description2" && cmUpdated.Version == "1.1");
    }
}*/