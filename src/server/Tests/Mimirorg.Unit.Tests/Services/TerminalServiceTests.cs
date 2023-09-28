using System;
using Mimirorg.Authentication.Exceptions;
using Mimirorg.Test.Setup;
using Mimirorg.Test.Setup.Fixtures;
using Tyle.Core.Common.Exceptions;
using TypeLibrary.Services.Services;
using Xunit;

namespace Mimirorg.Test.Unit.Services;

public class TerminalServiceTests : UnitTest<MimirorgCommonFixture>
{
    private readonly TerminalService _terminalService;

    public TerminalServiceTests(MimirorgCommonFixture fixture) : base(fixture)
    {
        _terminalService = new TerminalService(fixture.TerminalRepository.Object, fixture.Mapper.Object,
            fixture.TimedHookService.Object, fixture.LogService.Object, fixture.TerminalServiceLogger.Object,
            fixture.AttributeService.Object, fixture.AttributeRepository.Object,
            fixture.TerminalAttributeRepository.Object,
            fixture.HttpContextAccessor.Object, fixture.EmailService.Object, fixture.ClassifierRepository.Object,
            fixture.PurposeRepository.Object, fixture.MediumRepository.Object,
            fixture.TerminalClassifierRepository.Object);
    }

    [Fact]
    public void GetThrowsMimirorgNotFoundException()
    {
        Assert.Throws<MimirorgNotFoundException>(() => _terminalService.Get(Guid.NewGuid()));
    }

    [Fact]
    public void CreateThrowsExceptionOnNullInput()
    {
        Assert.ThrowsAsync<ArgumentNullException>(() => _terminalService.Create(null));
    }

    [Fact]
    public void UpdateThrowsMimirorgNotFoundException()
    {
        Assert.ThrowsAsync<MimirorgNotFoundException>(() => _terminalService.Update(Guid.NewGuid(), null));
    }

    [Fact]
    public void DeleteThrowsMimirorgNotFoundException()
    {
        Assert.ThrowsAsync<MimirorgNotFoundException>(() => _terminalService.Delete(Guid.NewGuid()));
    }
}