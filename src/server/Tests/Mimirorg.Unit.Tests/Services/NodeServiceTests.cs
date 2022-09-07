using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Mimirorg.Common.Exceptions;
using Mimirorg.Setup;
using Mimirorg.Setup.Fixtures;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using TypeLibrary.Services.Services;
using Xunit;

namespace Mimirorg.Unit.Tests.Services
{
    public class NodeServiceTests : UnitTest<MimirorgCommonFixture>
    {
        private readonly NodeService _nodeService;

        public NodeServiceTests(MimirorgCommonFixture fixture) : base(fixture)
        {
            _nodeService = new NodeService(Options.Create(fixture.ApplicationSettings), fixture.VersionService.Object, fixture.Mapper.Object, fixture.NodeRepository.Object, fixture.TimedHookService.Object);
        }

        [Fact]
        public async Task Get_Returns_MimirorgBadRequestException_On_Null_WhiteSpaceParam()
        {
            _ = await Assert.ThrowsAsync<MimirorgBadRequestException>(() => _nodeService.Get(null));
            _ = await Assert.ThrowsAsync<MimirorgBadRequestException>(() => _nodeService.Get(""));
            _ = await Assert.ThrowsAsync<MimirorgBadRequestException>(() => _nodeService.Get(" "));
        }

        [Fact]
        public async Task GetNode_No_Matching_Id_Throws_MimirorgNotFoundException()
        {
            _ = await Assert.ThrowsAsync<MimirorgNotFoundException>(() => _nodeService.Get("Stupid_Fake"));
        }

        [Fact]
        public async Task Create_Node_Returns_MimirorgBadRequestException_When_Null_Parameters()
        {
            _ = await Assert.ThrowsAsync<MimirorgBadRequestException>(() => _nodeService.Create(null, true));
        }

        [Theory]
        [InlineData("", "Fake_RdsName", "Fake_RdsCode", "Fake_PurposeName")]
        [InlineData(" ", "Fake_RdsName", "Fake_RdsCode", "Fake_PurposeName")]
        [InlineData(null, "Fake_RdsName", "Fake_RdsCode", "Fake_PurposeName")]
        [InlineData("Invalid_Node_Object", "", "Fake_RdsCode", "Fake_PurposeName")]
        [InlineData("Invalid_Node_Object", " ", "Fake_RdsCode", "Fake_PurposeName")]
        [InlineData("Invalid_Node_Object", null, "Fake_RdsCode", "Fake_PurposeName")]
        [InlineData("Invalid_Node_Object", "Fake_RdsName", "Fake_RdsCode", "")]
        [InlineData("Invalid_Node_Object", "Fake_RdsName", "Fake_RdsCode", " ")]
        [InlineData("Invalid_Node_Object", "Fake_RdsName", "Fake_RdsCode", null)]
        [InlineData("Invalid_Node_Object", "Fake_RdsName", "", "Fake_PurposeName")]
        [InlineData("Invalid_Node_Object", "Fake_RdsName", " ", "Fake_PurposeName")]
        [InlineData("Invalid_Node_Object", "Fake_RdsName", null, "Fake_PurposeName")]
        public async Task Create_Node_Returns_MimirorgBadRequestException_When_Missing_Parameters(string name, string rdsName, string rdsCode, string purposeName)
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

            _ = await Assert.ThrowsAsync<MimirorgBadRequestException>(() => _nodeService.Create(nodeToCreate, true));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task Create_Node_Returns_MimirorgBadRequestException_When_CompanyId_Is_Not_Greater_Than_Zero(int companyId)
        {
            var nodeToCreate = new NodeLibAm
            {
                Name = "Invalid_Node_Object",
                RdsName = "Fake_RdsName",
                RdsCode = "Fake_RdsCode",
                PurposeName = "Fake_PurposeName",
                Aspect = Aspect.NotSet,
                CompanyId = companyId
            };

            _ = await Assert.ThrowsAsync<MimirorgBadRequestException>(() => _nodeService.Create(nodeToCreate, true));
        }
    }
}