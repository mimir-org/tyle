using Microsoft.Extensions.DependencyInjection;
using Mimirorg.Common.Enums;
using Mimirorg.Test.Setup;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using TypeLibrary.Services.Contracts;
using Xunit;

namespace Mimirorg.Test.Integration.Services;

public class NodeServiceTests : IntegrationTest
{
    public NodeServiceTests(ApiWebApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Create_Node_Create_Node_When_Ok_Parameters()
    {
        var nodeService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<INodeService>();
        var logService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<ILogService>();

        var nodeAm = new NodeLibAm
        {
            Name = "Node2",
            RdsName = "RdsName",
            RdsCode = "RdsCode",
            PurposeName = "PurposeName",
            Description = "Description",
            Aspect = Aspect.NotSet,
            CompanyId = 1,
            NodeTerminals = new List<NodeTerminalLibAm>{
                new()
                {
                    TerminalId = 60427,
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
            ParentId = 1234,
            Version = "1.0"
        };

        var nodeCm = await nodeService.Create(nodeAm);

        Assert.NotNull(nodeCm);
        Assert.True(nodeCm.State == State.Draft);
        Assert.Equal(nodeAm.Name, nodeCm.Name);
        Assert.Equal(nodeAm.RdsName, nodeCm.RdsName);
        Assert.Equal(nodeAm.RdsCode, nodeCm.RdsCode);
        Assert.Equal(nodeAm.PurposeName, nodeCm.PurposeName);
        Assert.Equal(nodeAm.Aspect, nodeCm.Aspect);
        Assert.Equal(nodeAm.Description, nodeCm.Description);
        Assert.Equal(nodeAm.CompanyId, nodeCm.CompanyId);

        foreach (var am in nodeAm.NodeTerminals)
        {
            foreach (var cm in nodeCm.NodeTerminals)
            {
                Assert.Equal(am.TerminalId.ToString(), cm.Terminal.Id);
                Assert.Equal(am.MinQuantity, cm.MinQuantity);
                Assert.Equal(am.MaxQuantity, cm.MaxQuantity);
                Assert.Equal(am.ConnectorDirection.ToString(), cm.ConnectorDirection.ToString());
            }
        }

        Assert.Equal(nodeAm.SelectedAttributePredefined.First().Key, nodeCm.SelectedAttributePredefined.First().Key);
        Assert.Equal(nodeAm.SelectedAttributePredefined.First().IsMultiSelect, nodeCm.SelectedAttributePredefined.First().IsMultiSelect);
        Assert.Equal(nodeAm.SelectedAttributePredefined.First().Values.ToString(), nodeCm.SelectedAttributePredefined.First().Values.ToString());

        Assert.Equal(nodeAm.SelectedAttributePredefined.First().TypeReference, nodeCm.SelectedAttributePredefined.First().TypeReference);

        Assert.Equal(nodeAm.TypeReference, nodeCm.TypeReference);

        Assert.Equal(nodeAm.Symbol, nodeCm.Symbol);
        Assert.Equal(nodeAm.ParentId.ToString(), nodeCm.ParentId);

        var logCm = logService.Get().FirstOrDefault(x => x.ObjectId == nodeCm.Id && x.ObjectType == "NodeLibDm");

        Assert.True(logCm != null);
        Assert.Equal(nodeCm.Id, logCm.ObjectId);
        Assert.Equal(nodeCm.FirstVersionId, logCm.ObjectFirstVersionId);
        Assert.Equal(nodeCm.Name, logCm.ObjectName);
        Assert.Equal(nodeCm.Version, logCm.ObjectVersion);
        Assert.Equal(nodeCm.GetType().Name.Remove(nodeCm.GetType().Name.Length - 2, 2) + "Dm", logCm.ObjectType);
        Assert.Equal(LogType.State.ToString(), logCm.LogType.ToString());
        Assert.Equal(State.Draft.ToString(), logCm.LogTypeValue);
        Assert.NotNull(logCm.User);
        Assert.Equal("System.DateTime", logCm.Created.GetType().ToString());
        Assert.True(logCm.Created.Kind == DateTimeKind.Utc);
    }

    [Fact]
    public async Task GetLatestVersions_Nodes_Result_Ok()
    {
        var nodeAm = new NodeLibAm
        {
            Name = "Node4",
            RdsName = "RdsName",
            RdsCode = "RdsCode",
            PurposeName = "PurposeName",
            Description = "Description",
            Aspect = Aspect.NotSet,
            CompanyId = 1,
            Version = "1.0"
        };

        var nodeService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<INodeService>();
        var nodeCm = await nodeService.Create(nodeAm);

        nodeAm.Description = "Description v1.1";

        var nodeCmUpdated = await nodeService.Update(int.Parse(nodeCm.Id), nodeAm);

        Assert.True(nodeCm?.Description == "Description");
        Assert.True(nodeCm.Version == "1.0");
        Assert.True(nodeCmUpdated?.Description == "Description v1.1");
        Assert.True(nodeCmUpdated.Version == "1.1");
    }


    [Fact]
    public async Task Update_Node_Result_Ok()
    {
        var nodeAm = new NodeLibAm
        {
            Name = "Node6",
            RdsName = "RdsName",
            RdsCode = "RdsCode",
            PurposeName = "PurposeName",
            Description = "Description1",
            Aspect = Aspect.NotSet,
            CompanyId = 1,
            Version = "1.0"
        };

        var nodeService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<INodeService>();

        var cm = await nodeService.Create(nodeAm);
        nodeAm.Description = "Description2";
        var cmUpdated = await nodeService.Update(int.Parse(cm.Id), nodeAm);

        Assert.True(cm.Description == "Description1" && cm.Version == "1.0");
        Assert.True(cmUpdated.Description == "Description2" && cmUpdated.Version == "1.1");
    }
}