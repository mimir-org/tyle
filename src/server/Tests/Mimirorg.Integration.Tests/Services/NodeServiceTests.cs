using Microsoft.Extensions.DependencyInjection;
using Mimirorg.Common.Exceptions;
using Mimirorg.Setup;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Extensions;
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
        public async Task Create_Node_Create_Node_When_Ok_Parameters(string name, string rdsName, string rdsCode, string purposeName)
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
    }
}







//        [Theory]
//        [InlineData("Ok_Object", "Fake_Rds", "Fake_Purpose")]
//        public async Task Create_Node_Node_With_Attributes_Result_Ok(string name, string rds, string purpose)
//        {
//            var nodeToCreate = new NodeLibAm
//            {
//                Name = name,
//                RdsCode = rds,
//                PurposeName = purpose,
//                Aspect = Aspect.Function,
//                AttributeIdList = new List<string>
//                {
//                    "Fake_Attribute_A"
//                }
//            };


//        [Theory]
//        [InlineData("Ok_Object", "Fake_Rds", "Fake_Purpose")]
//        public async Task Create_Node_Node_With_Attributes_Result_Ok(string name, string rds, string purpose)
//        {
//            var nodeToCreate = new NodeLibAm
//            {
//                Name = name,
//                RdsCode = rds,
//                PurposeName = purpose,
//                Aspect = Aspect.Function,
//                AttributeIdList = new List<string>
//                {
//                    "Fake_Attribute_A"
//                }
//            };

//            var id = $"{name}-{rds}-{nodeToCreate.Aspect}-{nodeToCreate.Version}".CreateMd5();

//            var node = await _nodeService.Create(nodeToCreate);
//            Assert.NotNull(node);
//            Assert.Equal(name, node.Name);
//            Assert.Equal(rds, node.RdsCode);
//            Assert.Equal(purpose, node.PurposeName);
//            Assert.Equal(nodeToCreate.Aspect, node.Aspect);
//            Assert.Equal(id, node.Id);
//        }
//    }
//}