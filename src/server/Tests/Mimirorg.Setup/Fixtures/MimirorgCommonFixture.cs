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

namespace Mimirorg.Test.Setup.Fixtures
{
    public class MimirorgCommonFixture : IDisposable
    {
        // Common
        public MimirorgAuthSettings MimirorgAuthSettings = new();
        public ApplicationSettings ApplicationSettings = new();
        public Mock<IMapper> Mapper = new();

        // Repositories
        public Mock<INodeRepository> NodeRepository = new();
        public Mock<IQuantityDatumRepository> DatumRepository = new();
        public Mock<IAttributePredefinedRepository> AttributePredefinedRepository = new();
        public Mock<IAttributeReferenceRepository> AttributeReferenceRepository = new();

        // Services
        public Mock<ITimedHookService> TimedHookService = new();
        public Mock<ILogService> LogService = new();

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
                Attributes = new List<AttributeLibAm>
                {
                    new()
                    {
                        Name = "a1",
                        Iri = "http://rds.posccaesar.org/ontology/plm/rdl/PCA_a1",
                        Source = "PCA",
                        Units = new List<UnitLibAm>
                        {
                            new()
                            {
                                Name = "u1",
                                Iri = "http://rds.posccaesar.org/ontology/plm/rdl/PCA_u1",
                                IsDefault = true
                            },
                            new()
                            {
                                Name = "u2",
                                Iri = "http://rds.posccaesar.org/ontology/plm/rdl/PCA_u2",
                                IsDefault = false
                            }
                        }
                    }
                },
                NodeTerminals = new List<NodeTerminalLibAm>
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
                Attributes = nodeLibAm.Attributes.ConvertToString(),
                NodeTerminals = new List<NodeTerminalLibDm>
                {
                    new()
                    {
                        ConnectorDirection = ConnectorDirection.Input,
                        MinQuantity = 1,
                        MaxQuantity = int.MaxValue,
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

        public (TerminalLibAm am, TerminalLibDm dm) CreateTerminalTestData()
        {
            var typeRefs = new List<TypeReferenceAm>
            {
                new()
                {
                    Iri = "https://tyle.com",
                    Name = "XX"
                }
            };

            var terminalLibAm = new TerminalLibAm
            {
                Name = "AA",
                TypeReferences = typeRefs,
                Color = "#123",
                Attributes = new List<AttributeLibAm>
                {
                    new()
                    {
                        Name = "a1",
                        Iri = "http://rds.posccaesar.org/ontology/plm/rdl/PCA_a1",
                        Source = "PCA",
                        Units = new List<UnitLibAm>
                        {
                            new()
                            {
                                Name = "u1",
                                Iri = "http://rds.posccaesar.org/ontology/plm/rdl/PCA_u1",
                                IsDefault = true
                            },
                            new()
                            {
                                Name = "u2",
                                Iri = "http://rds.posccaesar.org/ontology/plm/rdl/PCA_u2",
                                IsDefault = false
                            }
                        }
                    }
                },
                Version = "1.0"
            };

            var terminalLibDm = new TerminalLibDm
            {
                Name = "AA",
                Color = "#123",
                Attributes = terminalLibAm.Attributes.ConvertToString(),
                Version = "1.0",
                TypeReferences = typeRefs.ConvertToString()
            };

            return (terminalLibAm, terminalLibDm);
        }

        public void Dispose()
        {

        }
    }
}