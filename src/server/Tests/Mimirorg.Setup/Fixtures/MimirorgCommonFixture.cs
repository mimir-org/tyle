using AutoMapper;
using Microsoft.Extensions.Logging;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Common.Models;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Moq;
using TypeLibrary.Data.Contracts;
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
    
    // Loggers
    public Mock<ILogger<AspectObjectService>> AspectObjectServiceLogger = new();

    // Repositories
    public Mock<IAspectObjectRepository> AspectObjectRepository = new();
    public Mock<IAttributeRepository> AttributeRepository = new();

    // Services
    public Mock<ITimedHookService> TimedHookService = new();
    public Mock<ILogService> LogService = new();
    public Mock<IApplicationSettingsRepository> ApplicationSettingsRepository = new();

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
            RdsName = "AA",
            RdsCode = "AA",
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
            ParentId = "123",
            TypeReference = "https://www.tyle.com/"
        };

        var aspectObjectLibDm = new AspectObjectLibDm
        {
            Id = "68313",
            Name = "AA",
            RdsName = "AA",
            RdsCode = "AA",
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
            ParentId = "123",
            TypeReference = "https://www.tyle.com/"
        };

        return (aspectObjectLibAm, aspectObjectLibDm);
    }

    public (TerminalLibAm am, TerminalLibDm dm) CreateTerminalTestData()
    {
        var terminalLibAm = new TerminalLibAm
        {
            Name = "AA",
            TypeReference = "https://www.tyle.com/",
            Color = "#123",
            Attributes = new List<string>()
        };

        var terminalLibDm = new TerminalLibDm
        {
            Name = "AA",
            Color = "#123",
            Attributes = new List<AttributeLibDm>(),
            TypeReference = "https://www.tyle.com/"
        };

        return (terminalLibAm, terminalLibDm);
    }

    public void Dispose()
    {

    }
}