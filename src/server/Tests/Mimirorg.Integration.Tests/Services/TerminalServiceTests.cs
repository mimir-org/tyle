using Microsoft.Extensions.DependencyInjection;
using Mimirorg.Common.Enums;
using Mimirorg.Test.Setup;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using TypeLibrary.Services.Contracts;
using Xunit;

namespace Mimirorg.Test.Integration.Services;

public class TerminalServiceTests : IntegrationTest
{
    public TerminalServiceTests(ApiWebApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Create_Terminal_Create_Terminal_When_Ok_Parameters()
    {
        var newAttribute = new AttributeLibAm
        {
            Name = "a11",
            Iri = "http://rds.posccaesar.org/ontology/plm/rdl/PCA_a11",
            Source = "PCA",
            Units = new List<UnitLibAm>
            {
                new()
                {
                    Name = "u11",
                    Iri = "http://rds.posccaesar.org/ontology/plm/rdl/PCA_u11",
                    IsDefault = true
                },
                new()
                {
                    Name = "u22",
                    Iri = "http://rds.posccaesar.org/ontology/plm/rdl/PCA_u22",
                    IsDefault = false
                }
            }
        };

        var terminalAm = new TerminalLibAm
        {
            Name = "TestTerminal2",
            ParentId = 1234,
            TypeReferences = new List<TypeReferenceAm>
            {
                new()
                {
                    Name = "TypeRef",
                    Iri = "https://url.com/1234567890",
                    Source = "https://source.com/1234567890",
                }
            },
            Color = "#123456",
            Description = "Description1",
            Attributes = new List<AttributeLibAm> { newAttribute },
            CompanyId = 1,
            Version = "1.0"
        };

        var terminalService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<ITerminalService>();
        var logService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<ILogService>();

        var terminalCm = await terminalService.Create(terminalAm);

        Assert.NotNull(terminalCm);
        Assert.True(terminalCm.State == State.Draft);
        Assert.Equal(terminalAm.ParentId, terminalCm.ParentId);

        Assert.Equal(terminalAm.TypeReferences.First().Iri, terminalCm.TypeReferences.First().Iri);
        Assert.Equal(terminalAm.TypeReferences.First().Name, terminalCm.TypeReferences.First().Name);
        Assert.Equal(terminalAm.TypeReferences.First().Source, terminalCm.TypeReferences.First().Source);

        Assert.Equal(terminalAm.Color, terminalCm.Color);
        Assert.Equal(terminalAm.Description, terminalCm.Description);
        Assert.Equal(terminalAm.Attributes.ToList()[0].Id, terminalCm.Attributes.ToList()[0].Id);
        Assert.Equal(terminalAm.CompanyId, terminalCm.CompanyId);

        var logCm = logService.Get().FirstOrDefault(x => x.ObjectId == terminalCm.Id);

        Assert.True(logCm != null);
        Assert.Equal(terminalCm.Id, logCm.ObjectId);
        Assert.Equal(terminalCm.FirstVersionId, logCm.ObjectFirstVersionId);
        Assert.Equal(terminalCm.Name, logCm.ObjectName);
        Assert.Equal(terminalCm.Version, logCm.ObjectVersion);
        Assert.Equal(terminalCm.GetType().Name.Remove(terminalCm.GetType().Name.Length - 2, 2) + "Dm", logCm.ObjectType);
        Assert.Equal(LogType.State.ToString(), logCm.LogType.ToString());
        Assert.Equal(State.Draft.ToString(), logCm.LogTypeValue);
        Assert.NotNull(logCm.User);
        Assert.Equal("System.DateTime", logCm.Created.GetType().ToString());
        Assert.True(logCm.Created.Kind == DateTimeKind.Utc);
    }

    [Fact]
    public async Task GetLatestVersions_Terminal_Result_Ok()
    {
        var terminalAm = new TerminalLibAm
        {
            Name = "TestTerminal3",
            ParentId = 1234,
            TypeReferences = null,
            Color = "#123456",
            Description = "Description v1.0",
            CompanyId = 1,
            Version = "1.0"
        };

        var terminalService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<ITerminalService>();
        var terminalLibCm = await terminalService.Create(terminalAm);

        terminalAm.Description = "Description v1.1";

        var terminalCmUpdated = await terminalService.Update(terminalLibCm.Id, terminalAm);

        Assert.True(terminalLibCm?.Description == "Description v1.0");
        Assert.True(terminalLibCm.Version == "1.0");
        Assert.True(terminalCmUpdated?.Description == "Description v1.1");
        Assert.True(terminalCmUpdated.Version == "1.1");
    }
}