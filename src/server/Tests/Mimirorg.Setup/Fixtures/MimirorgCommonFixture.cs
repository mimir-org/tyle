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
    public Mock<ILogger<BlockService>> BlockServiceLogger = new();

    // Repositories
    public Mock<IEfBlockRepository> BlockRepository = new();
    public Mock<IAttributeRepository> AttributeRepository = new();
    public Mock<IEfBlockTerminalRepository> BlockTerminalRepository = new();
    public Mock<IEfBlockAttributeRepository> BlockAttributeRepository = new();

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

    public (BlockLibAm am, BlockLibDm dm) CreateBlockTestData()
    {
        var blockLibAm = new BlockLibAm
        {
            Name = "AA",
            RdsId = "AA",
            Aspect = Aspect.Function,
            BlockTerminals = new List<BlockTerminalLibAm>
            {
                new()
                {
                    Direction = Direction.Input,
                    MinQuantity = 1,
                    MaxQuantity = int.MaxValue,
                    TerminalId = "123"
                },
                new()
                {
                    Direction = Direction.Input,
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

        var blockLibDm = new BlockLibDm
        {
            Id = "68313",
            Name = "AA",
            RdsId = "AA",
            Aspect = Aspect.Function,
            BlockTerminals = new List<BlockTerminalLibDm>
            {
                new()
                {
                    Direction = Direction.Input,
                    MinCount = 1,
                    MaxCount = int.MaxValue,
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

        return (blockLibAm, blockLibDm);
    }

    public void Dispose()
    {

    }
}