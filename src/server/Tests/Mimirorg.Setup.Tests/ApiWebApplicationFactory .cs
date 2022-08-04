using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TypeLibrary.Api;
using TypeLibrary.Data;

namespace Mimirorg.Setup.Tests
{
    public class ApiWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                // remove the existing context configuration
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<TypeLibraryDbContext>));
                if (descriptor != null)
                    services.Remove(descriptor);

                services.AddDbContext<TypeLibraryDbContext>(options => options.UseInMemoryDatabase("TestDB"));
                services.AddAuthentication("IntegrationUser").AddScheme<AuthenticationSchemeOptions, IntegrationTestAuthenticationHandler>("IntegrationUser", options => { });
            });
        }
    }
}