using Microsoft.Extensions.DependencyInjection;
using Mimirorg.Common.Exceptions;
using Mimirorg.Setup;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
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
            var interfaceAm = new InterfaceLibAm
            {
                Name = "Interface1",
                RdsName = "RdsName",
                RdsCode = "RdsCode",
                PurposeName = "PurposeName",
                Description = "Description",
                Aspect = Aspect.NotSet,
                CompanyId = 1,
                AttributeIdList = new List<string>
                {
                    "0646754DC953F5EDD4F6159CD993696D"
                }
            };

            var interfaceService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<IInterfaceService>();
            await interfaceService.Create(interfaceAm);
            Task Act() => interfaceService.Create(interfaceAm);
            _ = await Assert.ThrowsAsync<MimirorgDuplicateException>(Act);
        }

        [Fact]
        public async Task Create_Interface_Create_Interface_When_Ok_Parameters()
        {
            var interfaceAm = new InterfaceLibAm
            {
                Name = "Interface2",
                RdsName = "RdsName",
                RdsCode = "RdsCode",
                PurposeName = "PurposeName",
                Description = "Description",
                Aspect = Aspect.NotSet,
                CompanyId = 1,
                AttributeIdList = new List<string>
                {
                    "0646754DC953F5EDD4F6159CD993696D"
                }
            };

            var interfaceService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<IInterfaceService>();
            var interfaceCm = await interfaceService.Create(interfaceAm);

            Assert.NotNull(interfaceCm);
            Assert.Equal(interfaceAm.Id, interfaceCm.Id);
            Assert.Equal(interfaceAm.Name, interfaceCm.Name);
            Assert.Equal(interfaceAm.RdsName, interfaceCm.RdsName);
            Assert.Equal(interfaceAm.RdsCode, interfaceCm.RdsCode);
            Assert.Equal(interfaceAm.PurposeName, interfaceCm.PurposeName);
            Assert.Equal(interfaceAm.Aspect, interfaceCm.Aspect);
            Assert.Equal(interfaceAm.Description, interfaceCm.Description);
            Assert.Equal(interfaceAm.CompanyId, interfaceCm.CompanyId);
        }

        [Fact]
        public async Task Create_Interface_Interface_With_Attributes_Result_Ok()
        {
            var interfaceAm = new InterfaceLibAm
            {
                Name = "Interface3",
                RdsName = "RdsName",
                RdsCode = "RdsCode",
                PurposeName = "PurposeName",
                Description = "Description",
                Aspect = Aspect.NotSet,
                CompanyId = 1,
                AttributeIdList = new List<string>
                {
                    "0646754DC953F5EDD4F6159CD993696D"
                }
            };

            var interfaceService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<IInterfaceService>();
            var interfaceCm = await interfaceService.Create(interfaceAm);

            Assert.Equal(interfaceAm.Id, interfaceCm?.Id);
            Assert.Equal(interfaceAm.Id, interfaceCm?.Id);
            Assert.Equal(interfaceAm.AttributeIdList.ElementAt(0), interfaceCm?.Attributes.ElementAt(0).Id);
            Assert.Equal(interfaceAm.AttributeIdList.ElementAt(0), interfaceCm?.Attributes.ElementAt(0).Id);
        }

        [Fact]
        public async Task GetLatestVersions_Interface_Result_Ok()
        {
            var interfaceAm = new InterfaceLibAm
            {
                Name = "Interface4",
                RdsName = "RdsName",
                RdsCode = "RdsCode",
                PurposeName = "PurposeName",
                Description = "Description",
                Aspect = Aspect.NotSet,
                CompanyId = 1,
                AttributeIdList = new List<string>
                {
                    "0646754DC953F5EDD4F6159CD993696D"
                }
            };

            var interfaceService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<IInterfaceService>();
            var interfaceCm = await interfaceService.Create(interfaceAm);

            interfaceAm.Description = "Description v1.1";

            var interfaceCmUpdated = await interfaceService.Update(interfaceAm, interfaceAm.Id);

            Assert.True(interfaceCm?.Description == "Description");
            Assert.True(interfaceCm.Version == "1.0");
            Assert.True(interfaceCmUpdated?.Description == "Description v1.1");
            Assert.True(interfaceCmUpdated.Version == "1.1");
        }

        [Fact]
        public async Task Delete_Interface_Result_Ok()
        {
            var interfaceAm = new InterfaceLibAm
            {
                Name = "Interface5",
                RdsName = "RdsName",
                RdsCode = "RdsCode",
                PurposeName = "PurposeName",
                Description = "Description",
                Aspect = Aspect.NotSet,
                CompanyId = 1,
                AttributeIdList = new List<string>
                {
                    "0646754DC953F5EDD4F6159CD993696D"
                }
            };

            var interfaceService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<IInterfaceService>();
            var interfaceCm = await interfaceService.Create(interfaceAm);

            var isDeleted = await interfaceService.Delete(interfaceCm?.Id);
            var allInterfacesNotDeleted = await interfaceService.GetLatestVersions();

            Assert.True(isDeleted);
            Assert.True(string.IsNullOrEmpty(allInterfacesNotDeleted?.FirstOrDefault(x => x.Id == interfaceCm?.Id)?.Id));
        }
    }
}