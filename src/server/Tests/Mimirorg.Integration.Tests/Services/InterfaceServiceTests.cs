using AngleSharp.Dom;
using Microsoft.Extensions.DependencyInjection;
using Mimirorg.Common.Enums;
using Mimirorg.Common.Exceptions;
using Mimirorg.Setup;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Extensions;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Services.Contracts;
using Xunit;

namespace Mimirorg.Integration.Tests.Services
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
            var attributeService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<IAttributeService>();
            var terminalService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<ITerminalService>();

            var attributeAm = new AttributeLibAm
            {
                Name = "attribute123456111",
                Aspect = Aspect.Function,
                Discipline = Discipline.Electrical,
                Select = Select.MultiSelect,
                Description = "Description1",
                SelectValues = new List<string> { "value1", "VALUE2", "value3" },
                QuantityDatumRangeSpecifying = "Normal",
                QuantityDatumSpecifiedProvenance = "Calculated",
                QuantityDatumRegularitySpecified = "Absolute",
                QuantityDatumSpecifiedScope = "Design Datum",
                CompanyId = 1
            };

            var terminalAm = new TerminalLibAm
            {
                Name = "Terminal12525",
                Color = "#45678",
                CompanyId = 1
            };

            var attributeCm = await attributeService.Create(attributeAm);
            var terminalCm = await terminalService.Create(terminalAm, true);

            var interfaceAm = new InterfaceLibAm
            {
                Name = "Interface76867944",
                RdsName = "RdsName",
                RdsCode = "RdsCode",
                PurposeName = "PurposeName",
                Aspect = Aspect.NotSet,
                CompanyId = 1,
                TerminalId = terminalAm.Id
            };

            await interfaceService.Create(interfaceAm);
            Task Act() => interfaceService.Create(interfaceAm);
            _ = await Assert.ThrowsAsync<MimirorgDuplicateException>(Act);
        }

        [Fact]
        public async Task Create_Interface_Create_Interface_When_Ok_Parameters()
        {
            var interfaceService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<IInterfaceService>();
            var attributeService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<IAttributeService>();
            var terminalService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<ITerminalService>();

            var attributeAm = new AttributeLibAm
            {
                Name = "attribute123456",
                Aspect = Aspect.Function,
                Discipline = Discipline.Electrical,
                Select = Select.MultiSelect,
                Description = "Description1",
                SelectValues = new List<string> { "value1", "VALUE2", "value3" },
                QuantityDatumRangeSpecifying = "Normal",
                QuantityDatumSpecifiedProvenance = "Calculated",
                QuantityDatumRegularitySpecified = "Absolute",
                QuantityDatumSpecifiedScope = "Design Datum",
                CompanyId = 1
            };

            var terminalAm = new TerminalLibAm
            {
                Name = "Terminal1",
                Color = "#45678",
                CompanyId = 1
            };

            var attributeCm = await attributeService.Create(attributeAm);

            var interfaceParentAm = new InterfaceLibAm
            {
                Name = "InterfaceParent",
                RdsName = "RdsName",
                RdsCode = "RdsCode",
                PurposeName = "PurposeName",
                Aspect = Aspect.NotSet,
                CompanyId = 1,
                TerminalId = terminalAm.Id
            };

            var interfaceParentCm = await interfaceService.Create(interfaceParentAm);
            var terminalCm = await terminalService.Create(terminalAm, true);

            var interfaceAm = new InterfaceLibAm
            {
                Name = "Interface2",
                RdsName = "RdsName",
                RdsCode = "RdsCode",
                PurposeName = "PurposeName",
                Aspect = Aspect.NotSet,
                CompanyId = 1,
                TerminalId = terminalCm.Id,
                AttributeIdList = new List<string> { $"{attributeCm.Id}" },
                TypeReferences = new List<TypeReferenceAm>
                {
                    new()
                    {
                        Name = "TypeRef",
                        Iri = "https://url.com/1234567890",
                        Source = "https://source.com/1234567890",
                        Subs = new List<TypeReferenceSub>
                        {
                            new()
                            {
                                Name = "SubName",
                                Iri = "https://subIri.com/1234567890"
                            }
                        }
                    }
                },
                Description = "Description",
                ParentId = interfaceParentCm.Id
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
            Assert.Equal(interfaceAm.AttributeIdList.ToList().ConvertToString(), interfaceCm.Attributes.Select(x => x.Id).ToList().ConvertToString());

            Assert.Equal(interfaceAm.TypeReferences.First().Iri, interfaceCm.TypeReferences.First().Iri);
            Assert.Equal(interfaceAm.TypeReferences.First().Name, interfaceCm.TypeReferences.First().Name);
            Assert.Equal(interfaceAm.TypeReferences.First().Source, interfaceCm.TypeReferences.First().Source);

            Assert.Equal(interfaceAm.TypeReferences.First().Subs.First().Name, interfaceCm.TypeReferences.First().Subs.First().Name);
            Assert.Equal(interfaceAm.TypeReferences.First().Subs.First().Iri, interfaceCm.TypeReferences.First().Subs.First().Iri);
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
                CompanyId = 1
            };

            var terminalCm = await terminalService.Create(terminalAm, true);

            var interfaceAm = new InterfaceLibAm
            {
                Name = "Interface4",
                RdsName = "RdsName",
                RdsCode = "RdsCode",
                PurposeName = "PurposeName",
                Description = "Description",
                Aspect = Aspect.NotSet,
                CompanyId = 1,
                TerminalId = terminalCm.Id
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
                Name = "Terminal108909990",
                Color = "#45678",
                CompanyId = 1
            };

            var terminalCm = await terminalService.Create(terminalAm, true);

            var interfaceAm = new InterfaceLibAm
            {
                Name = "Interface6",
                RdsName = "RdsName",
                RdsCode = "RdsCode",
                PurposeName = "PurposeName",
                Description = "Description1",
                Aspect = Aspect.NotSet,
                CompanyId = 1,
                TerminalId = terminalCm.Id
            };



            var cm = await interfaceService.Create(interfaceAm);
            interfaceAm.Description = "Description2";
            var cmUpdated = await interfaceService.Update(interfaceAm);

            Assert.True(cm.Description == "Description1" && cm.Version == "1.0");
            Assert.True(cmUpdated.Description == "Description2" && cmUpdated.Version == "1.1");
        }
    }
}