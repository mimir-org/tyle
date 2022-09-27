using AutoMapper;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Common.Models;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Extensions;
using Mimirorg.TypeLibrary.Models.Application;
using Moq;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;
using TypeLibrary.Services.Contracts;

namespace Mimirorg.Setup.Fixtures
{
    public class MimirorgCommonFixture : IDisposable
    {
        // Common
        public MimirorgAuthSettings MimirorgAuthSettings = new();
        public ApplicationSettings ApplicationSettings = new();
        public Mock<IMapper> Mapper = new();

        // Repositories
        public Mock<INodeRepository> NodeRepository = new();
        public Mock<IAttributeRepository> AttributeRepository = new();
        public Mock<IQuantityDatumRepository> DatumRepository = new();
        public Mock<IAttributePredefinedRepository> AttributePredefinedRepository = new();
        public Mock<IAttributeReferenceRepository> AttributeReferenceRepository = new();

        // Services
        public Mock<IVersionService> VersionService = new();
        public Mock<ITimedHookService> TimedHookService = new();

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

        public (NodeLibAm am, NodeLibDm dm) CreateNodeTestData()
        {
            var typeRefs = new List<TypeReferenceAm>
            {
                new()
                {
                    Iri = "https://tyle.com",
                    Name = "XX"
                }
            };

            var nodeLibAm = new NodeLibAm
            {
                Name = "AA",
                RdsName = "AA",
                RdsCode = "AA",
                Aspect = Aspect.Function,
                SimpleIdList = new List<string>
                {
                    "123",
                    "555"
                },
                AttributeIdList = new List<string>
                {
                    "123",
                    "555"
                },
                NodeTerminals = new List<NodeTerminalLibAm>
                {
                    new()
                    {
                        ConnectorDirection = ConnectorDirection.Input,
                        Quantity = 1,
                        TerminalId = "123"
                    },
                    new()
                    {
                        ConnectorDirection = ConnectorDirection.Input,
                        Quantity = 1,
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
                TypeReferences = typeRefs
            };

            var id = $"AA-AA-{Aspect.Function}-1.0".CreateMd5();
            var terminalId = $"123-{ConnectorDirection.Input}".CreateMd5();

            var nodeLibDm = new NodeLibDm
            {
                Id = $"AA-AA-{Aspect.Function}-1.0".CreateMd5(),
                Name = "AA",
                RdsName = "AA",
                RdsCode = "AA",
                Aspect = Aspect.Function,
                Attributes = new List<AttributeLibDm>
                {
                    new()
                    {
                        Id = "123"
                    }
                },
                NodeTerminals = new List<NodeTerminalLibDm>
                {
                    new()
                    {
                        ConnectorDirection = ConnectorDirection.Input,
                        Quantity = 1,
                        TerminalId = "123",
                        Id = $"{terminalId}-{id}".CreateMd5()
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
                TypeReferences = typeRefs.ConvertToString()
            };

            return (nodeLibAm, nodeLibDm);
        }

        public (InterfaceLibAm am, InterfaceLibDm dm) CreateInterfaceTestData()
        {
            var typeRefs = new List<TypeReferenceAm>
            {
                new()
                {
                    Iri = "https://tyle.com",
                    Name = "XX"
                }
            };

            var interfaceLibAm = new InterfaceLibAm
            {
                Name = "AA",
                RdsName = "AA",
                RdsCode = "AA",
                Aspect = Aspect.Function,
                AttributeIdList = new List<string>
                {
                    "123",
                    "555"
                },
                ParentId = "123",
                TypeReferences = typeRefs
            };

            var interfaceLibDm = new InterfaceLibDm
            {
                Name = "AA",
                RdsName = "AA",
                RdsCode = "AA",
                Aspect = Aspect.Function,
                Attributes = new List<AttributeLibDm>
                {
                    new()
                    {
                        Id = "123"
                    }
                },
                ParentId = "123",
                TypeReferences = typeRefs.ConvertToString()
            };

            return (interfaceLibAm, interfaceLibDm);
        }

        public (TransportLibAm am, TransportLibDm dm) CreateTransportTestData()
        {
            var typeRefs = new List<TypeReferenceAm>
            {
                new()
                {
                    Iri = "https://tyle.com",
                    Name = "XX"
                }
            };

            var transportLibAm = new TransportLibAm
            {
                Name = "AA",
                RdsName = "AA",
                RdsCode = "AA",
                Aspect = Aspect.Function,
                AttributeIdList = new List<string>
                {
                    "123",
                    "555"
                },
                ParentId = "123",
                TypeReferences = typeRefs
            };

            var transportLibDm = new TransportLibDm
            {
                Name = "AA",
                RdsName = "AA",
                RdsCode = "AA",
                Aspect = Aspect.Function,
                Attributes = new List<AttributeLibDm>
                {
                    new()
                    {
                        Id = "123"
                    }
                },
                ParentId = "123",
                TypeReferences = typeRefs.ConvertToString()
            };

            return (transportLibAm, transportLibDm);
        }

        public void Dispose()
        {

        }
    }
}