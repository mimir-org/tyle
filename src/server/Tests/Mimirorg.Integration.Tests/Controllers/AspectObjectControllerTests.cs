using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Mimirorg.Test.Setup;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using TypeLibrary.Services.Contracts;
using Xunit;

namespace Mimirorg.Test.Integration.Controllers;

public class AspectObjectControllerTests : IntegrationTest
{
    public AspectObjectControllerTests(ApiWebApplicationFactory factory) : base(factory)
    {
    }

    [Theory]
    [InlineData("/v1/libraryaspectobject")]
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
    [InlineData("/v1/libraryaspectobject/")]
    public async Task GET_Id_Retrieves_Status_Ok(string endpoint)
    {
        var client = Factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(_ =>
            {

            });
        }).CreateClient(new WebApplicationFactoryClientOptions());

        const string guid = "2f9e0813-1067-472e-86ea-7c0b47a4eb18";

        // Ensure aspect object in fake database
        var aspectObjectToCreate = new AspectObjectLibAm
        {
            Name = $"{guid}_dummy_name",
            RdsName = $"{guid}_dummy_rds_name",
            RdsCode = $"{guid}_dummy_rds_code",
            PurposeName = $"{guid}_dummy_purpose_name",
            Aspect = Aspect.NotSet,
            CompanyId = 1,
            Version = "1.0"
        };

        using var scope = Factory.Server.Services.CreateScope();
        var aspectObjectService = scope.ServiceProvider.GetRequiredService<IAspectObjectService>();
        var createdAspectObject = await aspectObjectService.Create(aspectObjectToCreate);

        var response = await client.GetAsync(endpoint + createdAspectObject.Id);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Theory]
    [InlineData("/v1/libraryaspectobject/666666")]
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