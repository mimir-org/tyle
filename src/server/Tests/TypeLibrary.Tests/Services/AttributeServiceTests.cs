using TypeLibrary.Tests.Setup;
using Xunit;

namespace TypeLibrary.Tests.Services
{
    public class AttributeServiceTests : IntegrationTest
    {
        public AttributeServiceTests(ApiWebApplicationFactory fixture) : base(fixture)
        {
        }

        [Fact]
        public Task Create_Attributes_Throws_Bad_Request_When_Not_Valid()
        {
            _ = Factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {

                });
            });
            return Task.CompletedTask;
        }
    }
}