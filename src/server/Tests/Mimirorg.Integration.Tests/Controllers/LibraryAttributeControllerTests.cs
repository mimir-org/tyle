using System.Net;
using Mimirorg.Test.Setup;
using Xunit;

namespace Mimirorg.Test.Integration.Controllers;

public class LibraryAttributeControllerTests : IntegrationTest
{
    public LibraryAttributeControllerTests(ApiWebApplicationFactory fixture) : base(fixture)
    {
    }

    [Theory]
    [InlineData("/attributes")]
    public async Task GET_Retrieves_Status_Ok(string endpoint)
    {
        var response = await Client.GetAsync(endpoint);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}