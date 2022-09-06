using Microsoft.Extensions.DependencyInjection;
using Mimirorg.Common.Exceptions;
using Mimirorg.Setup;
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
                ParentId = null,
                TypeReferences = null,
                Color = "#123456",
                Description = "Description1",
                AttributeIdList = null,
                CompanyId = 1
            };

            var terminalService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<ITerminalService>();
            await terminalService.Create(terminalAm);
            Task Act() => terminalService.Create(terminalAm);
            _ = await Assert.ThrowsAsync<MimirorgDuplicateException>(Act);
        }

        [Fact]
        public async Task Create_Terminal_Create_Terminal_When_Ok_Parameters()
        {
            var terminalAm = new TerminalLibAm
            {
                Name = "TestTerminal2",
                ParentId = null,
                TypeReferences = null,
                Color = "#123456",
                Description = "Description1",
                AttributeIdList = new List<string>{ "F302FA9BD63AA991A91C6B9A88CE9691", "0DE08DEEB00409D554DB4B6C31AA34CC" },
                CompanyId = 1
            };

            var terminalService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<ITerminalService>();
            var terminalCm = await terminalService.Create(terminalAm);

            Assert.NotNull(terminalCm);
            Assert.Equal(terminalAm.Id, terminalCm.Id);
            Assert.Equal(terminalAm.ParentId, terminalCm.ParentId);
            Assert.Equal(terminalAm.TypeReferences.ConvertToString(), terminalCm.TypeReferences.ConvertToString());
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
                ParentId = null,
                TypeReferences = null,
                Color = "#123456",
                Description = "Description v1.0",
                AttributeIdList = new List<string> { "F302FA9BD63AA991A91C6B9A88CE9691", "0DE08DEEB00409D554DB4B6C31AA34CC" },
                CompanyId = 1
            };

            var terminalService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<ITerminalService>();
            var terminalLibCm = await terminalService.Create(terminalAm);

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
                ParentId = null,
                TypeReferences = null,
                Color = "#123456",
                Description = "Description1",
                CompanyId = 1
            };

            var terminalService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<ITerminalService>();

            var terminalCm = await terminalService.Create(terminalAm);
            var isDeleted = await terminalService.Delete(terminalCm?.Id);
            var allTerminalsNotDeleted = await terminalService.GetAll();
            var allTerminalsIncludeDeleted = await terminalService.GetAll(true);

            Assert.True(isDeleted);
            Assert.True(string.IsNullOrEmpty(allTerminalsNotDeleted?.FirstOrDefault(x => x.Id == terminalCm?.Id)?.Id));
            Assert.True(!string.IsNullOrEmpty(allTerminalsIncludeDeleted?.FirstOrDefault(x => x.Id == terminalCm?.Id)?.Id));
        }
    }
}