using Xunit;

namespace Mimirorg.Setup.Tests
{
    [Trait("Category", "Integration")]
    public abstract class IntegrationTest : IClassFixture<ApiWebApplicationFactory>
    {
        protected readonly ApiWebApplicationFactory Factory;
        protected readonly HttpClient Client;

        protected IntegrationTest(ApiWebApplicationFactory factory)
        {
            Factory = factory;
            Client = Factory.CreateClient();
        }
    }
}