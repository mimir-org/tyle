using System;
using System.Threading.Tasks;
using Mimirorg.Common.Exceptions;
using Mimirorg.Test.Setup;
using Mimirorg.Test.Setup.Fixtures;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using TypeLibrary.Services.Services;
using Xunit;

namespace Mimirorg.Test.Unit.Services;

public class AspectObjectServiceTests : UnitTest<MimirorgCommonFixture>
{
    private readonly AspectObjectService _aspectObjectService;

    public AspectObjectServiceTests(MimirorgCommonFixture fixture) : base(fixture)
    {
        _aspectObjectService = new AspectObjectService(fixture.Mapper.Object, fixture.AspectObjectRepository.Object, fixture.TimedHookService.Object, fixture.LogService.Object);
    }

    [Fact]
    public void Get_Returns_MimirorgBadRequestException_On_ZeroParam()
    {
        _ = Assert.Throws<MimirorgNotFoundException>(() => _aspectObjectService.Get(0));
    }

    [Fact]
    public void GetAspectObject_No_Matching_Id_Throws_MimirorgNotFoundException()
    {
        _ = Assert.Throws<MimirorgNotFoundException>(() => _aspectObjectService.Get(6666666));
    }

    [Fact]
    public async Task Create_AspectObject_Returns_MimirorgBadRequestException_When_Null_Parameters()
    {
        _ = await Assert.ThrowsAsync<ArgumentNullException>(() => _aspectObjectService.Create(null));
    }

    [Theory]
    [InlineData("", "Fake_RdsName", "Fake_RdsCode", "Fake_PurposeName")]
    [InlineData(" ", "Fake_RdsName", "Fake_RdsCode", "Fake_PurposeName")]
    [InlineData(null, "Fake_RdsName", "Fake_RdsCode", "Fake_PurposeName")]
    [InlineData("Invalid_Aspect_Object", "", "Fake_RdsCode", "Fake_PurposeName")]
    [InlineData("Invalid_Aspect_Object", " ", "Fake_RdsCode", "Fake_PurposeName")]
    [InlineData("Invalid_Aspect_Object", null, "Fake_RdsCode", "Fake_PurposeName")]
    [InlineData("Invalid_Aspect_Object", "Fake_RdsName", "Fake_RdsCode", "")]
    [InlineData("Invalid_Aspect_Object", "Fake_RdsName", "Fake_RdsCode", " ")]
    [InlineData("Invalid_Aspect_Object", "Fake_RdsName", "Fake_RdsCode", null)]
    [InlineData("Invalid_Aspect_Object", "Fake_RdsName", "", "Fake_PurposeName")]
    [InlineData("Invalid_Aspect_Object", "Fake_RdsName", " ", "Fake_PurposeName")]
    [InlineData("Invalid_Aspect_Object", "Fake_RdsName", null, "Fake_PurposeName")]
    public async Task Create_AspectObject_Returns_MimirorgBadRequestException_When_Missing_Parameters(string name, string rdsName, string rdsCode, string purposeName)
    {
        var aspectObjectToCreate = new AspectObjectLibAm
        {
            Name = name,
            RdsName = rdsName,
            RdsCode = rdsCode,
            PurposeName = purposeName,
            Aspect = Aspect.NotSet,
            CompanyId = 1
        };

        _ = await Assert.ThrowsAsync<MimirorgBadRequestException>(() => _aspectObjectService.Create(aspectObjectToCreate));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task Create_AspectObject_Returns_MimirorgBadRequestException_When_CompanyId_Is_Not_Greater_Than_Zero(int companyId)
    {
        var aspectObjectToCreate = new AspectObjectLibAm
        {
            Name = "Invalid_Aspect_Object",
            RdsName = "Fake_RdsName",
            RdsCode = "Fake_RdsCode",
            PurposeName = "Fake_PurposeName",
            Aspect = Aspect.NotSet,
            CompanyId = companyId
        };

        _ = await Assert.ThrowsAsync<MimirorgBadRequestException>(() => _aspectObjectService.Create(aspectObjectToCreate));
    }
}