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
        _aspectObjectService = new AspectObjectService(fixture.Mapper.Object, fixture.AspectObjectRepository.Object, fixture.AttributeRepository.Object, fixture.TimedHookService.Object, fixture.LogService.Object, fixture.AspectObjectServiceLogger.Object);
    }

    [Fact]
    public void Get_Returns_MimirorgBadRequestException_On_NullParam()
    {
        _ = Assert.Throws<MimirorgNotFoundException>(() => _aspectObjectService.Get(null));
    }

    [Fact]
    public void GetAspectObject_No_Matching_Id_Throws_MimirorgNotFoundException()
    {
        _ = Assert.Throws<MimirorgNotFoundException>(() => _aspectObjectService.Get("6666666"));
    }

    [Fact]
    public async Task Create_AspectObject_Returns_MimirorgBadRequestException_When_Null_Parameters()
    {
        _ = await Assert.ThrowsAsync<ArgumentNullException>(() => _aspectObjectService.Create(null));
    }

    [Theory]
    [InlineData("", "Fake_RdsId", "Fake_PurposeName")]
    [InlineData(" ", "Fake_RdsId", "Fake_PurposeName")]
    [InlineData(null, "Fake_RdsId", "Fake_PurposeName")]
    [InlineData("Invalid_Aspect_Object", "", "Fake_PurposeName")]
    [InlineData("Invalid_Aspect_Object", " ", "Fake_PurposeName")]
    [InlineData("Invalid_Aspect_Object", null, "Fake_PurposeName")]
    [InlineData("Invalid_Aspect_Object", "Fake_RdsId", "")]
    [InlineData("Invalid_Aspect_Object", "Fake_RdsId", " ")]
    [InlineData("Invalid_Aspect_Object", "Fake_RdsId", null)]
    public async Task Create_AspectObject_Returns_MimirorgBadRequestException_When_Missing_Parameters(string name, string rdsId, string purposeName)
    {
        var aspectObjectToCreate = new AspectObjectLibAm
        {
            Name = name,
            RdsId = rdsId,
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
            RdsId = "Fake_RdsId",
            PurposeName = "Fake_PurposeName",
            Aspect = Aspect.NotSet,
            CompanyId = companyId
        };

        _ = await Assert.ThrowsAsync<MimirorgBadRequestException>(() => _aspectObjectService.Create(aspectObjectToCreate));
    }
}