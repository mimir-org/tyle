using AutoMapper;
using Microsoft.AspNetCore.Http;
using Mimirorg.Authentication.Models;
using Moq;
using TypeLibrary.Core.Blocks;
using TypeLibrary.Core.Common;
using TypeLibrary.Core.Terminals;
using TypeLibrary.Services.Attributes;
using TypeLibrary.Services.Blocks;
using TypeLibrary.Services.Blocks.Requests;
using TypeLibrary.Services.Common;
using TypeLibrary.Services.Terminals;

namespace Mimirorg.Test.Setup.Fixtures;

public class MimirorgCommonFixture : IDisposable
{
    // Common
    public MimirorgAuthSettings MimirorgAuthSettings = new();
    public ApplicationSettings ApplicationSettings = new();
    public Mock<IMapper> Mapper = new();
    public Mock<IHttpContextAccessor> HttpContextAccessor = new();

    // Repositories
    public Mock<IBlockRepository> BlockRepository = new();
    public Mock<ITerminalRepository> TerminalRepository = new();
    public Mock<IAttributeRepository> AttributeRepository = new();
    public Mock<IClassifierRepository> ClassifierRepository = new();
    public Mock<IMediumRepository> MediumRepository = new();
    public Mock<IPurposeRepository> PurposeRepository = new();
    public Mock<IPredicateRepository> PredicateRepository = new();
    public Mock<IUnitRepository> UnitRepository = new();

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

    public (BlockTypeRequest am, BlockType dm) CreateBlockTestData()
    {
        var reusableGuid = Guid.NewGuid();

        var blockLibAm = new BlockTypeRequest
        {
            Name = "AA",
            Aspect = Aspect.Function,
            Terminals = new List<TerminalTypeReferenceRequest>()
            {
                new()
                {
                    Direction = Direction.Input,
                    MinCount = 1,
                    MaxCount = int.MaxValue,
                    TerminalId = reusableGuid
                },
                new()
                {
                    Direction = Direction.Input,
                    MinCount = 1,
                    MaxCount = int.MaxValue,
                    TerminalId = Guid.NewGuid()
                }
            }
        };

        var blockLibDm = new BlockType
        {
            CreatedBy = "Unknown",
            Name = "AA",
            Version = "",
            Aspect = Aspect.Function,
            Terminals = new List<BlockTerminalTypeReference>()
            {
                new()
                {
                    Direction = Direction.Input,
                    MinCount = 1,
                    MaxCount = int.MaxValue,
                    TerminalId = reusableGuid
                }
            }
        };

        return (blockLibAm, blockLibDm);
    }

    public void Dispose()
    {

    }
}