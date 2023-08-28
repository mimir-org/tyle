/*using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Mimirorg.Test.Setup;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using TypeLibrary.Services.Contracts;
using Xunit;

namespace Mimirorg.Test.Integration.Controllers;

public class LibraryBlockControllerTests : IntegrationTest
{
    public LibraryBlockControllerTests(ApiWebApplicationFactory factory) : base(factory)
    {
    }

    [Theory]
    [InlineData("/v1/libraryblock")]
    public async Task GET_Retrieves_Status_Ok(string endpoint)
    {
        var client = Factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(_ =>
            {

            });
        }).CreateClient(new WebApplicationFactoryClientOptions());


        var response = await client.GetAsync(endpoint);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Theory]
    [InlineData("/v1/libraryblock/")]
    public async Task GET_Id_Retrieves_Status_Ok(string endpoint)
    {
        var client = Factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(_ =>
            {

            });
        }).CreateClient(new WebApplicationFactoryClientOptions());

        const string guid = "2f9e0813-1067-472e-86ea-7c0b47a4eb18";

        // Ensure block in fake database
        var blockToCreate = new BlockLibAm
        {
            Name = $"{guid}_dummy_name",
            RdsId = "rds-id",
            PurposeName = $"{guid}_dummy_purpose_name",
            Aspect = Aspect.NotSet,
            CompanyId = 1,
            Version = "1.0"
        };

        using var scope = Factory.Server.Services.CreateScope();
        var blockService = scope.ServiceProvider.GetRequiredService<IBlockService>();
        var createdBlock = await blockService.Create(blockToCreate);

        var response = await client.GetAsync(endpoint + createdBlock.Id);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Theory]
    [InlineData("/v1/libraryblock/666666")]
    public async Task GET_Id_Retrieves_Status_No_Content(string endpoint)
    {
        var client = Factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(_ =>
            {

            });
        }).CreateClient(new WebApplicationFactoryClientOptions());

        var response = await client.GetAsync(endpoint);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}*/