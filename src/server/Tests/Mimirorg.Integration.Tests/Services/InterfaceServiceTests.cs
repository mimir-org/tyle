using Microsoft.Extensions.DependencyInjection;
using Mimirorg.Common.Enums;
using Mimirorg.Common.Exceptions;
using Mimirorg.Test.Setup;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using TypeLibrary.Services.Contracts;
using Xunit;

namespace Mimirorg.Test.Integration.Services
{
    public class InterfaceServiceTests : IntegrationTest
    {
        public InterfaceServiceTests(ApiWebApplicationFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task Create_Interface_Returns_MimirorgDuplicateException_When_Already_Exist()
        {
            var interfaceService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<IInterfaceService>();

            var terminalAm = new TerminalLibAm
            {
                Name = "Terminal12525",
                Color = "#45678",
                CompanyId = 1,
                Version = "1.0"
            };

            var interfaceAm = new InterfaceLibAm
            {
                Name = "Interface76867944",
                RdsName = "RdsName",
                RdsCode = "RdsCode",
                PurposeName = "PurposeName",
                Aspect = Aspect.NotSet,
                CompanyId = 1,
                TerminalId = terminalAm.Id,
                Version = "1.0"
            };

            await interfaceService.Create(interfaceAm);
            Task Act() => interfaceService.Create(interfaceAm);
            _ = await Assert.ThrowsAsync<MimirorgDuplicateException>(Act);
        }

        [Fact]
        public async Task Create_Interface_Create_Interface_When_Ok_Parameters()
        {
            var interfaceService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<IInterfaceService>();
            var terminalService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<ITerminalService>();

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
                Name = "Terminal1",
                Color = "#45678",
                CompanyId = 1,
                Version = "1.0"
            };

            var interfaceParentAm = new InterfaceLibAm
            {
                Name = "InterfaceParent",
                RdsName = "RdsName",
                RdsCode = "RdsCode",
                PurposeName = "PurposeName",
                Aspect = Aspect.NotSet,
                CompanyId = 1,
                TerminalId = terminalAm.Id,
                Version = "1.0"
            };

            var interfaceParentCm = await interfaceService.Create(interfaceParentAm);
            var terminalCm = await terminalService.Create(terminalAm);

            var interfaceAm = new InterfaceLibAm
            {
                Name = "Interface2",
                RdsName = "RdsName",
                RdsCode = "RdsCode",
                PurposeName = "PurposeName",
                Aspect = Aspect.NotSet,
                CompanyId = 1,
                TerminalId = terminalCm.Id,
                Attributes = new List<AttributeLibAm> { newAttribute },
                TypeReferences = new List<TypeReferenceAm>
                {
                    new()
                    {
                        Name = "TypeRef",
                        Iri = "https://url.com/1234567890",
                        Source = "https://source.com/1234567890"
                    }
                },
                Description = "Description",
                ParentId = interfaceParentCm.Id,
                Version = "1.0"
            };

            var interfaceCm = await interfaceService.Create(interfaceAm);

            Assert.NotNull(interfaceCm);
            Assert.True(interfaceCm.State == State.Draft);
            Assert.Equal(interfaceAm.Id, interfaceCm.Id);
            Assert.Equal(interfaceAm.Name, interfaceCm.Name);
            Assert.Equal(interfaceAm.RdsName, interfaceCm.RdsName);
            Assert.Equal(interfaceAm.RdsCode, interfaceCm.RdsCode);
            Assert.Equal(interfaceAm.PurposeName, interfaceCm.PurposeName);
            Assert.Equal(interfaceAm.Description, interfaceCm.Description);
            Assert.Equal(interfaceAm.Aspect, interfaceCm.Aspect);
            Assert.Equal(interfaceAm.CompanyId, interfaceCm.CompanyId);
            Assert.Equal(interfaceAm.TerminalId, interfaceCm.TerminalId);
            Assert.Equal(interfaceAm.Attributes.ToList()[0].Id, interfaceCm.Attributes.ToList()[0].Id);

            Assert.Equal(interfaceAm.TypeReferences.First().Iri, interfaceCm.TypeReferences.First().Iri);
            Assert.Equal(interfaceAm.TypeReferences.First().Name, interfaceCm.TypeReferences.First().Name);
            Assert.Equal(interfaceAm.TypeReferences.First().Source, interfaceCm.TypeReferences.First().Source);

            Assert.Equal(interfaceAm.ParentId, interfaceCm.ParentId);
        }

        [Fact]
        public async Task GetLatestVersions_Interface_Result_Ok()
        {
            var interfaceService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<IInterfaceService>();
            var terminalService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<ITerminalService>();

            var terminalAm = new TerminalLibAm
            {
                Name = "Terminal1009990",
                Color = "#45678",
                CompanyId = 1,
                Version = "1.0"
            };

            var terminalCm = await terminalService.Create(terminalAm);

            var interfaceAm = new InterfaceLibAm
            {
                Name = "Interface4",
                RdsName = "RdsName",
                RdsCode = "RdsCode",
                PurposeName = "PurposeName",
                Description = "Description",
                Aspect = Aspect.NotSet,
                CompanyId = 1,
                TerminalId = terminalCm.Id,
                Version = "1.0"
            };

            var interfaceCm = await interfaceService.Create(interfaceAm);

            interfaceAm.Description = "Description v1.1";

            var interfaceCmUpdated = await interfaceService.Update(interfaceAm);

            Assert.True(interfaceCm?.Description == "Description");
            Assert.True(interfaceCm.Version == "1.0");
            Assert.True(interfaceCmUpdated?.Description == "Description v1.1");
            Assert.True(interfaceCmUpdated.Version == "1.1");
        }

        [Fact]
        public async Task Update_Interface_Result_Ok()
        {
            var terminalService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<ITerminalService>();
            var interfaceService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<IInterfaceService>();

            var terminalAm = new TerminalLibAm
            {
                Name = "Terminal108909990x4",
                Color = "#45678",
                CompanyId = 1,
                Version = "1.0"
            };

            var terminalCm = await terminalService.Create(terminalAm);

            var interfaceAm = new InterfaceLibAm
            {
                Name = "Interface6",
                RdsName = "RdsName",
                RdsCode = "RdsCode",
                PurposeName = "PurposeName",
                Description = "Description1",
                Aspect = Aspect.NotSet,
                CompanyId = 1,
                TerminalId = terminalCm.Id,
                Version = "1.0"
            };



            var cm = await interfaceService.Create(interfaceAm);
            interfaceAm.Description = "Description2";
            var cmUpdated = await interfaceService.Update(interfaceAm);

            Assert.True(cm.Description == "Description1" && cm.Version == "1.0");
            Assert.True(cmUpdated.Description == "Description2" && cmUpdated.Version == "1.1");
        }
    }
}