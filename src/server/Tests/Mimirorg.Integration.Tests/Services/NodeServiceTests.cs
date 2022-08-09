using Microsoft.Extensions.DependencyInjection;
using Mimirorg.Common.Exceptions;
using Mimirorg.Setup;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Services.Contracts;
using Xunit;

namespace Mimirorg.Integration.Tests.Services
{
    public class NodeServiceTests : IntegrationTest
    {
        private NodeLibAm _node1Am;
        private NodeLibAm _node2Am;
        private NodeLibCm _node1Cm;
        private NodeLibCm _node2Cm;
        private readonly INodeService _nodeService;

        public NodeServiceTests(ApiWebApplicationFactory factory) : base(factory)
        {
            _nodeService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<INodeService>();
            _ = PopulateInMemoryDatabase();
        }

        [Fact]
        public async Task Create_Node_Returns_MimirorgDuplicateException_When_Already_Exist()
        {
            var nodeToCreate = new NodeLibAm
            {
                Name = "Node1",
                RdsName = "RdsName1",
                RdsCode = "RdsCode1",
                PurposeName = "PurposeName1",
                Description = "Description1 v1.0",
                Aspect = Aspect.NotSet,
                CompanyId = 1,
                AttributeIdList = new List<string>
                {
                    "0646754DC953F5EDD4F6159CD993696D"
                }
            };

            Task Act() => _nodeService.Create(nodeToCreate);
            _ = await Assert.ThrowsAsync<MimirorgDuplicateException>(Act);
        }

        [Fact]
        public async Task Create_Node_Create_Node_When_Ok_Parameters()
        {
            var allNodes = await _nodeService.GetAll() as List<NodeLibCm> ?? new List<NodeLibCm>();
            var node1 = allNodes.FirstOrDefault(x => x.Name == _node1Am.Name);

            Assert.NotNull(node1);
            Assert.Equal(_node1Am.Id, node1.Id);
            Assert.Equal(_node1Am.Name, node1.Name);
            Assert.Equal(_node1Am.RdsName, node1.RdsName);
            Assert.Equal(_node1Am.RdsCode, node1.RdsCode);
            Assert.Equal(_node1Am.PurposeName, node1.PurposeName);
            Assert.Equal(_node1Am.Aspect, node1.Aspect);
            Assert.Equal(_node1Am.Description, node1.Description);
            Assert.Equal(_node1Am.CompanyId, node1.CompanyId);
        }

        [Fact]
        public async Task Create_Node_Node_With_Attributes_Result_Ok()
        {
            var allNodes = await _nodeService.GetAll() as List<NodeLibCm> ?? new List<NodeLibCm>();
            
            var node1 = allNodes.FirstOrDefault(x => x.Name == _node1Am.Name);
            var node2 = allNodes.FirstOrDefault(x => x.Name == _node2Am.Name);

            Assert.Equal(_node1Am.Id, node1?.Id);
            Assert.Equal(_node2Am.Id, node2?.Id);
            Assert.Equal(_node1Am.AttributeIdList.ElementAt(0), node1?.Attributes.ElementAt(0).Id);
            Assert.Equal(_node2Am.AttributeIdList.ElementAt(0), node2?.Attributes.ElementAt(0).Id);
        }

        [Fact]
        public async Task GetLatestVersions_Nodes_Result_Ok()
        {
            var allNodes = await _nodeService.GetAll() as List<NodeLibCm> ?? new List<NodeLibCm>();
            var latestVersions = await _nodeService.GetLatestVersions() as List<NodeLibCm> ?? new List<NodeLibCm>();
            
            Assert.True(allNodes.Count is 4);
            Assert.True(latestVersions.Count is 2);
            Assert.True(latestVersions.FirstOrDefault(x => x.FirstVersionId == _node1Cm.Id)?.Version == "1.1");
            Assert.True(latestVersions.FirstOrDefault(x => x.FirstVersionId == _node2Cm.Id)?.Version == "1.1");
        }

        private async Task PopulateInMemoryDatabase()
        {
            _node1Am = new NodeLibAm
            {
                Name = "Node1",
                RdsName = "RdsName1",
                RdsCode = "RdsCode1",
                PurposeName = "PurposeName1",
                Description = "Description1 v1.0",
                Aspect = Aspect.NotSet,
                CompanyId = 1,
                AttributeIdList = new List<string>
                {
                    "0646754DC953F5EDD4F6159CD993696D"
                }
            };

            _node2Am = new NodeLibAm
            {
                Name = "Node2",
                RdsName = "RdsName2",
                RdsCode = "RdsCode2",
                PurposeName = "PurposeName2",
                Description = "Description2 v1.0",
                Aspect = Aspect.NotSet,
                CompanyId = 1,
                AttributeIdList = new List<string>
                {
                    "0646754DC953F5EDD4F6159CD993696D"
                }
            };

            _node1Cm = await _nodeService.Create(_node1Am);
            _node2Cm = await _nodeService.Create(_node2Am);

            _node1Am.Description = "Description1 v1.1";
            _node2Am.Description = "Description2 v1.1";

            await _nodeService.Update(_node1Am, _node1Am.Id);
            await _nodeService.Update(_node2Am, _node2Am.Id);
        }
    }
}