using Microsoft.Extensions.DependencyInjection;
using Mimirorg.Test.Setup;
using TypeLibrary.Services.Contracts;
using Xunit;

// ReSharper disable InconsistentNaming

namespace Mimirorg.Test.Integration.Services;

public class AttributeServiceTests : IntegrationTest
{
    public AttributeServiceTests(ApiWebApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task DatumDataReceiveOk()
    {
        using var scope = Factory.Server.Services.CreateScope();
        var attributeService = scope.ServiceProvider.GetRequiredService<IAttributeService>();

        var rangeSpecifying = await attributeService.GetQuantityDatumRangeSpecifying();
        var regularitySpecified = await attributeService.GetQuantityDatumRegularitySpecified();
        var specifiedProvenance = await attributeService.GetQuantityDatumSpecifiedProvenance();
        var specifiedScope = await attributeService.GetQuantityDatumSpecifiedScope();

        Assert.True(rangeSpecifying != null);
        Assert.True(regularitySpecified != null);
        Assert.True(specifiedProvenance != null);
        Assert.True(specifiedScope != null);

        Assert.True(rangeSpecifying.Any());
        Assert.True(regularitySpecified.Any());
        Assert.True(specifiedProvenance.Any());
        Assert.True(specifiedScope.Any());
    }
}