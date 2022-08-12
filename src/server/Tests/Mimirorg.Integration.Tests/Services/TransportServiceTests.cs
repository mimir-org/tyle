using Microsoft.Extensions.DependencyInjection;
using Mimirorg.Common.Exceptions;
using Mimirorg.Setup;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using TypeLibrary.Services.Contracts;
using Xunit;

namespace Mimirorg.Integration.Tests.Services
{
    public class TransportServiceTests : IntegrationTest
    {
        public TransportServiceTests(ApiWebApplicationFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task Create_Transport_Returns_MimirorgDuplicateException_When_Already_Exist()
        {
            var transportAm = new TransportLibAm
            {
                Name = "Transport1",
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

            var transportService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<ITransportService>();
            await transportService.Create(transportAm);
            Task Act() => transportService.Create(transportAm);
            _ = await Assert.ThrowsAsync<MimirorgDuplicateException>(Act);
        }

        [Fact]
        public async Task Create_Transport_Create_Transport_When_Ok_Parameters()
        {
            var transportAm = new TransportLibAm
            {
                Name = "Transport2",
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

            var transportService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<ITransportService>();
            var transportCm = await transportService.Create(transportAm);

            Assert.NotNull(transportCm);
            Assert.Equal(transportAm.Id, transportCm.Id);
            Assert.Equal(transportAm.Name, transportCm.Name);
            Assert.Equal(transportAm.RdsName, transportCm.RdsName);
            Assert.Equal(transportAm.RdsCode, transportCm.RdsCode);
            Assert.Equal(transportAm.PurposeName, transportCm.PurposeName);
            Assert.Equal(transportAm.Aspect, transportCm.Aspect);
            Assert.Equal(transportAm.Description, transportCm.Description);
            Assert.Equal(transportAm.CompanyId, transportCm.CompanyId);
        }

        [Fact]
        public async Task Create_Transport_Transport_With_Attributes_Result_Ok()
        {
            var transportAm = new TransportLibAm
            {
                Name = "Transport3",
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

            var transportService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<ITransportService>();
            var transportCm = await transportService.Create(transportAm);

            Assert.Equal(transportAm.Id, transportCm?.Id);
            Assert.Equal(transportAm.Id, transportCm?.Id);
            Assert.Equal(transportAm.AttributeIdList.ElementAt(0), transportCm?.Attributes.ElementAt(0).Id);
            Assert.Equal(transportAm.AttributeIdList.ElementAt(0), transportCm?.Attributes.ElementAt(0).Id);
        }

        [Fact]
        public async Task GetLatestVersions_Transport_Result_Ok()
        {
            var transportAm = new TransportLibAm
            {
                Name = "Transport4",
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

            var transportService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<ITransportService>();
            var transportCm = await transportService.Create(transportAm);

            transportAm.Description = "Description v1.1";

            var transportCmUpdated = await transportService.Update(transportAm, transportAm.Id);

            Assert.True(transportCm?.Description == "Description");
            Assert.True(transportCm.Version == "1.0");
            Assert.True(transportCmUpdated?.Description == "Description v1.1");
            Assert.True(transportCmUpdated.Version == "1.1");
        }

        [Fact]
        public async Task Delete_Transport_Result_Ok()
        {
            var transportAm = new TransportLibAm
            {
                Name = "Transport5",
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

            var transportService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<ITransportService>();
            
            var transportCm = await transportService.Create(transportAm);
            var isDeleted = await transportService.Delete(transportCm?.Id);
            var allTransportsNotDeleted = await transportService.GetLatestVersions();

            Assert.True(isDeleted);
            Assert.True(string.IsNullOrEmpty(allTransportsNotDeleted?.FirstOrDefault(x => x.Id == transportCm?.Id)?.Id));
        }
    }
}