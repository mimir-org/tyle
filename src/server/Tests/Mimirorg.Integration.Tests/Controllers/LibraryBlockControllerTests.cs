using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Mimirorg.Test.Setup;
using TypeLibrary.Core.Common;
using TypeLibrary.Services.Blocks;
using TypeLibrary.Services.Blocks.Requests;
using Xunit;

namespace Mimirorg.Test.Integration.Controllers;

public class LibraryBlockControllerTests : IntegrationTest
{
    public LibraryBlockControllerTests(ApiWebApplicationFactory factory) : base(factory)
    {
    }

    [Theory]
    [InlineData("/v1/blocks")]
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
    [InlineData("/v1/blocks/")]
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
        var blockToCreate = new BlockTypeRequest
        {
            Name = $"{guid}_dummy_name",
            Aspect = Aspect.Function
        };

        using var scope = Factory.Server.Services.CreateScope();
        var blockRepository = scope.ServiceProvider.GetRequiredService<IBlockRepository>();
        var createdBlock = await blockRepository.Create(blockToCreate);

        var response = await client.GetAsync(endpoint + createdBlock.Id);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Theory]
    [InlineData("/v1/block/66666666-6666-6666-6666-666666666666")]
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
}