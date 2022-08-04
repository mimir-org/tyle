using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Mimirorg.Common.Exceptions;
using Mimirorg.Setup.Tests;
using Mimirorg.Setup.Tests.Fixtures;
using TypeLibrary.Services.Services;
using Xunit;

namespace Mimirorg.Unit.Tests.Services
{
    public class NodeServiceTests : UnitTest<MimirorgCommonFixture>
    {
        private readonly MimirorgCommonFixture _fixture;
        private readonly NodeService _nodeService;

        public NodeServiceTests(MimirorgCommonFixture fixture) : base(fixture)
        {
            _fixture = fixture;
            _nodeService = new NodeService(Options.Create(fixture.ApplicationSettings), fixture.VersionService.Object, fixture.Mapper.Object, fixture.NodeRepository.Object, fixture.TimedHookService.Object);
        }

        [Fact]
        public async Task Get_Returns_MimirorgBadRequestException_On_Null_WhiteSpaceParam()
        {
            _ = await Assert.ThrowsAsync<MimirorgBadRequestException>(() => _nodeService.Get(null));
            _ = await Assert.ThrowsAsync<MimirorgBadRequestException>(() => _nodeService.Get(""));
            _ = await Assert.ThrowsAsync<MimirorgBadRequestException>(() => _nodeService.Get(" "));
        }

        [Theory]
        [InlineData("Stupid_Fake")]
        public async Task GetNode_No_Matching_Id_Throws_MimirorgNotFoundException(string id)
        {
            _ = await Assert.ThrowsAsync<MimirorgNotFoundException>(() => _nodeService.Get(id));
        }
    }
}

//using System.Collections.Generic;
//using System.Threading.Tasks;
//using Mimirorg.Common.Exceptions;
//using Mimirorg.TypeLibrary.Enums;
//using Mimirorg.TypeLibrary.Extensions;
//using Mimirorg.TypeLibrary.Models.Application;
//using TypeLibrary.Services.Contracts;
//using Xunit;

//namespace TypeLibrary.Services.Tests
//{
//    public class LibraryServiceTests
//    {
//        private readonly INodeService _nodeService;

//        public LibraryServiceTests(INodeService nodeService)
//        {
//            _nodeService = nodeService;
//        }





//        [Theory]
//        [InlineData("", "Fake_Rds", "Fake_Purpose")]
//        [InlineData(" ", "Fake_Rds", "Fake_Purpose")]
//        [InlineData(null, "Fake_Rds", "Fake_Purpose")]
//        [InlineData("Invalid_Node_Object", "", "Fake_Purpose")]
//        [InlineData("Invalid_Node_Object", " ", "Fake_Purpose")]
//        [InlineData("Invalid_Node_Object", null, "Fake_Purpose")]
//        [InlineData("Invalid_Node_Object", "Fake_Rds", "")]
//        [InlineData("Invalid_Node_Object", "Fake_Rds", " ")]
//        [InlineData("Invalid_Node_Object", "Fake_Rds", null)]
//        [InlineData("Invalid_Node_Object", "Fake_Rds", "Fake_Purpose")]
//        public async Task Create_Node_Returns_MimirorgBadRequestException_When_Missing_Parameters(string name, string rds, string purpose)
//        {
//            var nodeToCreate = new NodeLibAm
//            {
//                Name = name,
//                RdsCode = rds,
//                PurposeName = purpose,
//            };

//            _ = await Assert.ThrowsAsync<MimirorgBadRequestException>(() => _nodeService.Create(nodeToCreate));
//        }

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

//        [Fact]
//        public async Task Create_Node_Returns_MimirorgNullReferenceException_When_Null_Parameters()
//        {
//            _ = await Assert.ThrowsAsync<MimirorgNullReferenceException>(() => _nodeService.Create(null));
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
