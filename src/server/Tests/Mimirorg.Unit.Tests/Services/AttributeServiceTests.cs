using System;
using Mimirorg.Authentication.Exceptions;
using Mimirorg.Test.Setup;
using Mimirorg.Test.Setup.Fixtures;
using Tyle.Core.Common.Exceptions;
using TypeLibrary.Services.Services;
using Xunit;

namespace Mimirorg.Test.Unit.Services;

public class AttributeServiceTests : UnitTest<MimirorgCommonFixture>
{
    private readonly AttributeService _attributeService;

    public AttributeServiceTests(MimirorgCommonFixture fixture) : base(fixture)
    {
        _attributeService = new AttributeService(fixture.Mapper.Object, fixture.AttributeRepository.Object,
            fixture.TimedHookService.Object, fixture.LogService.Object, fixture.HttpContextAccessor.Object,
            fixture.EmailService.Object, fixture.PredicateRepository.Object, fixture.UnitRepository.Object,
            fixture.AttributeUnitRepository.Object, fixture.ValueConstraintRepository.Object);
    }

    [Fact]
    public void GetThrowsMimirorgNotFoundException()
    {
        Assert.Throws<MimirorgNotFoundException>(() => _attributeService.Get(Guid.NewGuid()));
    }

    [Fact]
    public void CreateThrowsExceptionOnNullInput()
    {
        Assert.ThrowsAsync<ArgumentNullException>(() => _attributeService.Create(null));
    }

    [Fact]
    public void UpdateThrowsMimirorgNotFoundException()
    {
        Assert.ThrowsAsync<MimirorgNotFoundException>(() => _attributeService.Update(Guid.NewGuid(), null));
    }

    [Fact]
    public void DeleteThrowsMimirorgNotFoundException()
    {
        Assert.ThrowsAsync<MimirorgNotFoundException>(() => _attributeService.Delete(Guid.NewGuid()));
    }
}