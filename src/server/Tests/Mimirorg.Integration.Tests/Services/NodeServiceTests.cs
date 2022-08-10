using Microsoft.Extensions.DependencyInjection;
using Mimirorg.Common.Exceptions;
using Mimirorg.Setup;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using TypeLibrary.Services.Contracts;
using Xunit;

namespace Mimirorg.Integration.Tests.Services
{
    public class NodeServiceTests : IntegrationTest
    {
        public NodeServiceTests(ApiWebApplicationFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task Create_Node_Returns_MimirorgDuplicateException_When_Already_Exist()
        {
            var nodeAm = new NodeLibAm
            {
                Name = "Node1",
                RdsName = "RdsName",
                RdsCode = "RdsCode",
                PurposeName = "PurposeName",
                Description = "Description",
                Aspect = Aspect.NotSet,
                CompanyId = 1,
                AttributeIdList = new List<string>
                {
                    "0646754DC953F5EDD4F6159CD993696D"
                }
            };

            var nodeService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<INodeService>();
            await nodeService.Create(nodeAm);
            Task Act() => nodeService.Create(nodeAm);
            _ = await Assert.ThrowsAsync<MimirorgDuplicateException>(Act);
        }

        [Fact]
        public async Task Create_Node_Create_Node_When_Ok_Parameters()
        {
            var nodeAm = new NodeLibAm
            {
                Name = "Node2",
                RdsName = "RdsName",
                RdsCode = "RdsCode",
                PurposeName = "PurposeName",
                Description = "Description",
                Aspect = Aspect.NotSet,
                CompanyId = 1,
                AttributeIdList = new List<string>
                {
                    "0646754DC953F5EDD4F6159CD993696D"
                }
            };

            var nodeService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<INodeService>();
            var nodeCm = await nodeService.Create(nodeAm);

            Assert.NotNull(nodeCm);
            Assert.Equal(nodeAm.Id, nodeCm.Id);
            Assert.Equal(nodeAm.Name, nodeCm.Name);
            Assert.Equal(nodeAm.RdsName, nodeCm.RdsName);
            Assert.Equal(nodeAm.RdsCode, nodeCm.RdsCode);
            Assert.Equal(nodeAm.PurposeName, nodeCm.PurposeName);
            Assert.Equal(nodeAm.Aspect, nodeCm.Aspect);
            Assert.Equal(nodeAm.Description, nodeCm.Description);
            Assert.Equal(nodeAm.CompanyId, nodeCm.CompanyId);
        }

        [Fact]
        public async Task Create_Node_Node_With_Attributes_Result_Ok()
        {
            var nodeAm = new NodeLibAm
            {
                Name = "Node3",
                RdsName = "RdsName",
                RdsCode = "RdsCode",
                PurposeName = "PurposeName",
                Description = "Description",
                Aspect = Aspect.NotSet,
                CompanyId = 1,
                AttributeIdList = new List<string>
                {
                    "0646754DC953F5EDD4F6159CD993696D"
                }
            };

            var nodeService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<INodeService>();
            var nodeCm = await nodeService.Create(nodeAm);

            Assert.Equal(nodeAm.Id, nodeCm?.Id);
            Assert.Equal(nodeAm.Id, nodeCm?.Id);
            Assert.Equal(nodeAm.AttributeIdList.ElementAt(0), nodeCm?.Attributes.ElementAt(0).Id);
            Assert.Equal(nodeAm.AttributeIdList.ElementAt(0), nodeCm?.Attributes.ElementAt(0).Id);
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
                AttributeIdList = new List<string>
                {
                    "0646754DC953F5EDD4F6159CD993696D"
                }
            };

            var nodeService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<INodeService>();
            var nodeCm = await nodeService.Create(nodeAm);

            nodeAm.Description = "Description v1.1";

            var nodeCmUpdated = await nodeService.Update(nodeAm, nodeAm.Id);

            Assert.True(nodeCm?.Description == "Description");
            Assert.True(nodeCm.Version == "1.0");
            Assert.True(nodeCmUpdated?.Description == "Description v1.1");
            Assert.True(nodeCmUpdated.Version == "1.1");
        }
    }
}