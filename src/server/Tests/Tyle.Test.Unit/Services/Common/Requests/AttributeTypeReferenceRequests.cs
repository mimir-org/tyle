using System.ComponentModel.DataAnnotations;
using Microsoft.IdentityModel.Tokens;
using Tyle.Application.Common.Requests;
using Tyle.Test.Setup;
using Tyle.Test.Setup.Fixtures;
using Xunit;

namespace Tyle.Test.Unit.Services.Common.Requests;

public class TypeReferenceRequestTests : UnitTest<RequestTestFixture>
{
    private readonly IServiceProvider _serviceProvider;

    public TypeReferenceRequestTests(RequestTestFixture fixture) : base(fixture)
    {
        _serviceProvider = fixture.ServiceProvider.Object;
    }

    [Theory]
    [InlineData(0, 1, true)]
    [InlineData(1, 5, true)]
    [InlineData(3, 1, false)]
    [InlineData(4, 0, false)]
    [InlineData(1, null, true)]
    [InlineData(0, null, true)]
    public void MinMaxCountValidatesCorrectly(int min, int? max, bool result)
    {
        var terminalAttributeRequest = new AttributeTypeReferenceRequest
        {
            MinCount = min,
            MaxCount = max,
            AttributeId = Guid.NewGuid()
        };

        var validationContext = new ValidationContext(terminalAttributeRequest, _serviceProvider, null);

        var results = terminalAttributeRequest.Validate(validationContext);

        Assert.Equal(result, results.IsNullOrEmpty());
    }
}