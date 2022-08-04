using Mimirorg.Setup;

namespace Mimirorg.Integration.Tests.Services
{
    public class NodeServiceTests : IntegrationTest
    {
        public NodeServiceTests(ApiWebApplicationFactory factory) : base(factory)
        {
        }
    }
}


//        [Theory]
//        [InlineData("Invalid_Node_Object", "Fake_Rds", "Fake_Purpose")]
//        public async Task Create_Node_Returns_MimirorgDuplicateException_When_Already_Exist(string name, string rds, string purpose)
//        {
//            var nodeToCreate = new NodeLibAm
//            {
//                Name = name,
//                RdsCode = rds,
//                PurposeName = purpose,
//            };

//            await _nodeService.Create(nodeToCreate);
//            _ = await Assert.ThrowsAsync<MimirorgDuplicateException>(() => _nodeService.Create(nodeToCreate));
//        }


//        [Theory]
//        [InlineData("Ok_Object_1", "Fake_Rds", "Fake_Purpose")]
//        [InlineData("Ok_Object_1_2", "Fake_Rds", "Fake_Purpose")]
//        [InlineData("Ok_Object_1_3", "Fake_Rds", "Fake_Purpose")]
//        [InlineData("Ok_Object_1_4", "Fake_Rds", "Fake_Purpose")]
//        [InlineData("Ok_Object_1_5", "Fake_Rds", "Fake_Purpose")]
//        public async Task Create_Node_Create_Node_When_Ok_Parameters(string name, string rds, string purpose)
//        {
//            var nodeToCreate = new NodeLibAm
//            {
//                Name = name,
//                RdsCode = rds,
//                PurposeName = purpose,
//                Aspect = Aspect.Function
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