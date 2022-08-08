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
            var nodeToCreate = new NodeLibAm
            {
                Name = "NODE_A_DUPLICATE_56kai6",
                RdsName = "Fake_RdsName",
                RdsCode = "Fake_RdsCode",
                PurposeName = "Fake_PurposeName",
                Aspect = Aspect.NotSet,
                CompanyId = 1
            };

            using var scope = Factory.Server.Services.CreateScope();
            var nodeService = scope.ServiceProvider.GetRequiredService<INodeService>();
            await nodeService.Create(nodeToCreate);

            //Act
            Task Act() => nodeService.Create(nodeToCreate);

            //Assert
            _ = await Assert.ThrowsAsync<MimirorgDuplicateException>(Act);
        }

        [Theory]
        [InlineData("Fake_Node_245_1", "Fake_RdsName", "Fake_RdsCode", "Fake_PurposeName")]
        [InlineData("Fake_Node_245_2", "Fake_RdsName_2", "Fake_RdsCode_2", "Fake_PurposeName_2")]
        public async Task Create_Node_Create_Node_When_Ok_Parameters(string name, string rdsName, string rdsCode,
            string purposeName)
        {
            var nodeToCreate = new NodeLibAm
            {
                Name = name,
                RdsName = rdsName,
                RdsCode = rdsCode,
                PurposeName = purposeName,
                Aspect = Aspect.NotSet,
                CompanyId = 1
            };

            using var scope = Factory.Server.Services.CreateScope();
            var nodeService = scope.ServiceProvider.GetRequiredService<INodeService>();
            var node = await nodeService.Create(nodeToCreate);

            Assert.NotNull(node);
            Assert.Equal(name, node.Name);
            Assert.Equal(rdsName, node.RdsName);
            Assert.Equal(rdsCode, node.RdsCode);
            Assert.Equal(purposeName, node.PurposeName);
            Assert.Equal(nodeToCreate.Aspect, node.Aspect);
            Assert.Equal(nodeToCreate.Id, node.Id);
        }

        [Theory]
        [InlineData("Fake_Node_68912_1", "Fake_RdsName", "Fake_RdsCode", "Fake_PurposeName")]
        public async Task Create_Node_Node_With_Attributes_Result_Ok(string name, string rdsName, string rdsCode, string purposeName)
        {
            var nodeToCreate = new NodeLibAm
            {
                Name = name,
                RdsName = rdsName,
                RdsCode = rdsCode,
                PurposeName = purposeName,
                Aspect = Aspect.NotSet,
                CompanyId = 1,
                AttributeIdList = new List<string>
                {
                    "0646754DC953F5EDD4F6159CD993696D"
                }
            };

            using var scope = Factory.Server.Services.CreateScope();
            var nodeService = scope.ServiceProvider.GetRequiredService<INodeService>();
            var node = await nodeService.Create(nodeToCreate);
            Assert.Equal(nodeToCreate.Id, node.Id);
        }
    }
}