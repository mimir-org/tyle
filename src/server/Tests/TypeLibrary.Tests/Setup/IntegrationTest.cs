using Xunit;

namespace TypeLibrary.Tests.Setup
{
    [Trait("Category", "Integration")]
    public abstract class IntegrationTest : IClassFixture<ApiWebApplicationFactory>
    {
        protected readonly ApiWebApplicationFactory Factory;
        protected readonly HttpClient Client;

        protected IntegrationTest(ApiWebApplicationFactory fixture)
        {
            Factory = fixture;
            Client = Factory.CreateClient();
        }
    }
}