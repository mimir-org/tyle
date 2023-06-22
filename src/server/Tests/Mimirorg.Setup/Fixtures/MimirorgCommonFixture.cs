using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Common.Models;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Moq;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;
using TypeLibrary.Services.Contracts;
using TypeLibrary.Services.Services;

namespace Mimirorg.Test.Setup.Fixtures;

public class MimirorgCommonFixture : IDisposable
{
    // Common
    public MimirorgAuthSettings MimirorgAuthSettings = new();
    public ApplicationSettings ApplicationSettings = new();
    public Mock<IMapper> Mapper = new();
    public Mock<IHttpContextAccessor> HttpContextAccessor = new();

    // Loggers
    public Mock<ILogger<AspectObjectService>> AspectObjectServiceLogger = new();

    // Repositories
    public Mock<IEfAspectObjectRepository> AspectObjectRepository = new();
    public Mock<IAttributeRepository> AttributeRepository = new();
    public Mock<IEfAspectObjectTerminalRepository> AspectObjectTerminalRepository = new();
    public Mock<IEfAspectObjectAttributeRepository> AspectObjectAttributeRepository = new();

    // Services
    public Mock<IAttributeService> AttributeService = new();
    public Mock<ITerminalService> TerminalService = new();
    public Mock<IRdsService> RdsService = new();
    public Mock<ITimedHookService> TimedHookService = new();
    public Mock<ILogService> LogService = new();
    public Mock<IEmailService> EmailService = new();

    public MimirorgCommonFixture()
    {
        ApplicationSettings.ApplicationSemanticUrl = @"http://localhost:5001/v1/ont";
        ApplicationSettings.ApplicationUrl = @"http://localhost:5001";
        MimirorgAuthSettings.ApplicationUrl = @"http://localhost:5001";
        MimirorgAuthSettings.RequireDigit = true;
        MimirorgAuthSettings.RequireNonAlphanumeric = true;
        MimirorgAuthSettings.RequireUppercase = true;
        MimirorgAuthSettings.RequiredLength = 10;
    }

    public (AspectObjectLibAm am, AspectObjectLibDm dm) CreateAspectObjectTestData()
    {
        var aspectObjectLibAm = new AspectObjectLibAm
        {
            Name = "AA",
            RdsId = "AA",
            Aspect = Aspect.Function,
            AspectObjectTerminals = new List<AspectObjectTerminalLibAm>
            {
                new()
                {
                    ConnectorDirection = ConnectorDirection.Input,
                    MinQuantity = 1,
                    MaxQuantity = int.MaxValue,
                    TerminalId = "123"
                },
                new()
                {
                    ConnectorDirection = ConnectorDirection.Input,
                    MinQuantity = 1,
                    MaxQuantity = int.MaxValue,
                    TerminalId = "555"
                }
            },
            SelectedAttributePredefined = new List<SelectedAttributePredefinedLibAm>
            {
                new()
                {
                    Key = "123"
                },
                new()
                {
                    Key = "555"
                }
            },
            TypeReference = "https://www.tyle.com/"
        };

        var aspectObjectLibDm = new AspectObjectLibDm
        {
            Id = "68313",
            Name = "AA",
            RdsId = "AA",
            Aspect = Aspect.Function,
            AspectObjectTerminals = new List<AspectObjectTerminalLibDm>
            {
                new()
                {
                    ConnectorDirection = ConnectorDirection.Input,
                    MinQuantity = 1,
                    MaxQuantity = int.MaxValue,
                    TerminalId = "123",
                    Id = "74853"
                }
            },
            SelectedAttributePredefined = new List<SelectedAttributePredefinedLibDm>
            {
                new()
                {
                    Key = "123"
                }
            },
            TypeReference = "https://www.tyle.com/"
        };

        return (aspectObjectLibAm, aspectObjectLibDm);
    }

    public void Dispose()
    {

    }
}