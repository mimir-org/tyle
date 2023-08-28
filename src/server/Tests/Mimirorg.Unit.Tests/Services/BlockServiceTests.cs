/*using Mimirorg.Common.Exceptions;
using Mimirorg.Test.Setup;
using Mimirorg.Test.Setup.Fixtures;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using System;
using System.Threading.Tasks;
using TypeLibrary.Services.Services;
using Xunit;

namespace Mimirorg.Test.Unit.Services;

public class BlockServiceTests : UnitTest<MimirorgCommonFixture>
{
    private readonly BlockService _blockService;

    public BlockServiceTests(MimirorgCommonFixture fixture) : base(fixture)
    {
        _blockService = new BlockService(fixture.Mapper.Object, fixture.BlockRepository.Object, fixture.AttributeRepository.Object, fixture.BlockTerminalRepository.Object, fixture.BlockAttributeRepository.Object, fixture.TerminalService.Object, fixture.AttributeService.Object, fixture.RdsService.Object, fixture.TimedHookService.Object, fixture.LogService.Object, fixture.BlockServiceLogger.Object, fixture.HttpContextAccessor.Object, fixture.EmailService.Object);
    }

    [Fact]
    public void Get_Returns_MimirorgBadRequestException_On_NullParam()
    {
        _ = Assert.Throws<MimirorgNotFoundException>(() => _blockService.Get(null));
    }

    [Fact]
    public void GetBlock_No_Matching_Id_Throws_MimirorgNotFoundException()
    {
        _ = Assert.Throws<MimirorgNotFoundException>(() => _blockService.Get("6666666"));
    }

    [Fact]
    public async Task Create_Block_Returns_MimirorgBadRequestException_When_Null_Parameters()
    {
        _ = await Assert.ThrowsAsync<ArgumentNullException>(() => _blockService.Create(null));
    }

    [Theory]
    [InlineData("", "Fake_RdsId", "Fake_PurposeName")]
    [InlineData(" ", "Fake_RdsId", "Fake_PurposeName")]
    [InlineData(null, "Fake_RdsId", "Fake_PurposeName")]
    [InlineData("Invalid_Block", "", "Fake_PurposeName")]
    [InlineData("Invalid_Block", " ", "Fake_PurposeName")]
    [InlineData("Invalid_Block", null, "Fake_PurposeName")]
    [InlineData("Invalid_Block", "Fake_RdsId", "")]
    [InlineData("Invalid_Block", "Fake_RdsId", " ")]
    [InlineData("Invalid_Block", "Fake_RdsId", null)]
    public async Task Create_Block_Returns_MimirorgBadRequestException_When_Missing_Parameters(string name, string rdsId, string purposeName)
    {
        var blockToCreate = new BlockLibAm
        {
            Name = name,
            RdsId = rdsId,
            PurposeName = purposeName,
            Aspect = Aspect.NotSet,
            CompanyId = 1
        };

        _ = await Assert.ThrowsAsync<MimirorgBadRequestException>(() => _blockService.Create(blockToCreate));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task Create_Block_Returns_MimirorgBadRequestException_When_CompanyId_Is_Not_Greater_Than_Zero(int companyId)
    {
        var blockToCreate = new BlockLibAm
        {
            Name = "Invalid_Block",
            RdsId = "Fake_RdsId",
            PurposeName = "Fake_PurposeName",
            Aspect = Aspect.NotSet,
            CompanyId = companyId
        };

        _ = await Assert.ThrowsAsync<MimirorgBadRequestException>(() => _blockService.Create(blockToCreate));
    }
}*/