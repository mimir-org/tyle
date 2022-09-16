using Microsoft.Extensions.DependencyInjection;
using Mimirorg.Common.Exceptions;
using Mimirorg.Setup;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Extensions;
using Mimirorg.TypeLibrary.Models.Application;
using TypeLibrary.Services.Contracts;
using Xunit;

namespace Mimirorg.Integration.Tests.Services
{
    public class TerminalServiceTests : IntegrationTest
    {
        public TerminalServiceTests(ApiWebApplicationFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task Create_Terminal_Returns_MimirorgDuplicateException_When_Already_Exist()
        {
            var terminalAm = new TerminalLibAm
            {
                Name = "TestTerminal1",
                ParentId = "1234",
                TypeReferences = null,
                Color = "#123456",
                Description = "Description1",
                AttributeIdList = null,
                CompanyId = 1
            };

            var terminalService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<ITerminalService>();
            await terminalService.Create(terminalAm, true);
            Task Act() => terminalService.Create(terminalAm, true);
            _ = await Assert.ThrowsAsync<MimirorgDuplicateException>(Act);
        }

        [Fact]
        public async Task Create_Terminal_Create_Terminal_When_Ok_Parameters()
        {
            var terminalAm = new TerminalLibAm
            {
                Name = "TestTerminal2",
                ParentId = "1234",
                TypeReferences = new List<TypeReferenceAm>
                {
                    new()
                    {
                        Name = "TypeRef",
                        Iri = "https://url.com/1234567890",
                        Source = "https://source.com/1234567890",
                        SubName = "SubName",
                        SubIri = "https://subIri.com/1234567890",

                    }
                },
                Color = "#123456",
                Description = "Description1",
                AttributeIdList = new List<string> { "003F35CF40F34ECDE4E7EB589C7E0A00", "015640ADCA6E63DDCFF7F6823FCCC784" },
                CompanyId = 1
            };

            var terminalService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<ITerminalService>();
            var terminalCm = await terminalService.Create(terminalAm, true);

            Assert.NotNull(terminalCm);
            Assert.True(terminalCm.State == State.Draft);
            Assert.Equal(terminalAm.Id, terminalCm.Id);
            Assert.Equal(terminalAm.ParentId, terminalCm.ParentId);

            foreach (var typeReferenceAm in terminalAm.TypeReferences.OrderBy(x => x.Name, StringComparer.InvariantCulture).ThenBy(x => x.SubName))
            {
                foreach (var typeReferenceCm in terminalCm.TypeReferences.OrderBy(x => x.Name, StringComparer.InvariantCulture).ThenBy(x => x.SubName))
                {
                    Assert.Equal(typeReferenceAm.Name, typeReferenceCm.Name);
                    Assert.Equal(typeReferenceAm.Source, typeReferenceCm.Source);
                    Assert.Equal(typeReferenceAm.Iri, typeReferenceCm.Iri);
                    Assert.Equal(typeReferenceAm.SubIri, typeReferenceCm.SubIri);
                    Assert.Equal(typeReferenceAm.SubName, typeReferenceCm.SubName);
                }
            }

            Assert.Equal(terminalAm.Color, terminalCm.Color);
            Assert.Equal(terminalAm.Description, terminalCm.Description);
            Assert.Equal(terminalAm.AttributeIdList.ToList().ConvertToString(), terminalCm.Attributes.Select(x => x.Id).ToList().ConvertToString());
            Assert.Equal(terminalAm.CompanyId, terminalCm.CompanyId);
        }

        [Fact]
        public async Task GetLatestVersions_Terminal_Result_Ok()
        {
            var terminalAm = new TerminalLibAm
            {
                Name = "TestTerminal3",
                ParentId = "1234",
                TypeReferences = null,
                Color = "#123456",
                Description = "Description v1.0",
                AttributeIdList = new List<string> { "003F35CF40F34ECDE4E7EB589C7E0A00", "015640ADCA6E63DDCFF7F6823FCCC784" },
                CompanyId = 1
            };

            var terminalService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<ITerminalService>();
            var terminalLibCm = await terminalService.Create(terminalAm, true);

            terminalAm.Description = "Description v1.1";

            var terminalCmUpdated = await terminalService.Update(terminalAm, terminalAm.Id);

            Assert.True(terminalLibCm?.Description == "Description v1.0");
            Assert.True(terminalLibCm.Version == "1.0");
            Assert.True(terminalCmUpdated?.Description == "Description v1.1");
            Assert.True(terminalCmUpdated.Version == "1.1");
        }

        [Fact]
        public async Task Delete_Terminal_Result_Ok()
        {
            var terminalAm = new TerminalLibAm
            {
                Name = "TestTerminal4",
                ParentId = "1234",
                TypeReferences = null,
                Color = "#123456",
                Description = "Description1",
                CompanyId = 1
            };

            var terminalService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<ITerminalService>();

            var terminalCm = await terminalService.Create(terminalAm, true);
            var isDeleted = await terminalService.Delete(terminalCm?.Id);
            var allTerminalsNotDeleted = await terminalService.GetAll();
            var allTerminalsIncludeDeleted = await terminalService.GetAll(true);

            Assert.True(isDeleted);
            Assert.True(string.IsNullOrEmpty(allTerminalsNotDeleted?.FirstOrDefault(x => x.Id == terminalCm?.Id)?.Id));
            Assert.True(!string.IsNullOrEmpty(allTerminalsIncludeDeleted?.FirstOrDefault(x => x.Id == terminalCm?.Id)?.Id));
        }

        [Fact]
        public async Task Update_Terminal_State_Result_Ok()
        {
            var terminalAm = new TerminalLibAm
            {
                Name = "TestTerminal5",
                ParentId = "1234",
                TypeReferences = null,
                Color = "#123456",
                Description = "Description1",
                CompanyId = 1
            };

            var terminalService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<ITerminalService>();

            var cm = await terminalService.Create(terminalAm, true);
            var cmUpdated = await terminalService.UpdateState(cm.Id, State.ApprovedCompany);

            Assert.True(cm.State != cmUpdated.State);
            Assert.True(cmUpdated.State == State.ApprovedCompany);
        }
    }
}