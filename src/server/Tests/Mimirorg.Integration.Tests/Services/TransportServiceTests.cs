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
    public class TransportServiceTests : IntegrationTest
    {
        public TransportServiceTests(ApiWebApplicationFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task Create_Transport_Returns_MimirorgDuplicateException_When_Already_Exist()
        {
            var transportService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<ITransportService>();
            var terminalService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<ITerminalService>();

            var terminalAm = new TerminalLibAm
            {
                Name = "Terminal1078vb09990",
                Color = "#45678",
                CompanyId = 1
            };

            var terminalCm = await terminalService.Create(terminalAm);

            var transportAm = new TransportLibAm
            {
                Name = "Transport1",
                RdsName = "RdsName",
                RdsCode = "RdsCode",
                PurposeName = "PurposeName",
                Description = "Description1",
                Aspect = Aspect.NotSet,
                CompanyId = 1, TerminalId = terminalCm.Id
            };

            await transportService.Create(transportAm);
            Task Act() => transportService.Create(transportAm);
            _ = await Assert.ThrowsAsync<MimirorgDuplicateException>(Act);
        }

        [Fact]
        public async Task Create_Transport_Create_Transport_When_Ok_Parameters()
        {
            var transportParentAm = new TransportLibAm
            {
                Name = "TransportParent",
                RdsName = "RdsName",
                RdsCode = "RdsCode",
                PurposeName = "PurposeName",
                Aspect = Aspect.NotSet,
                CompanyId = 1,
                TerminalId = "8EBC5811473E87602FB0C18A100BD53C"
            };

            var transportService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<ITransportService>();
            var transportParentCm = await transportService.Create(transportParentAm);

            var transportAm = new TransportLibAm
            {
                Name = "Transport2",
                RdsName = "RdsName",
                RdsCode = "RdsCode",
                PurposeName = "PurposeName",
                Description = "Description",
                Aspect = Aspect.NotSet,
                CompanyId = 1,
                TerminalId = "8EBC5811473E87602FB0C18A100BD53C",
                AttributeIdList = new List<string>
                {
                    "CA20DF193D58238C3C557A0316C15533"
                },
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
                ParentId = transportParentCm.Id
            };

            var transportCm = await transportService.Create(transportAm);

            Assert.NotNull(transportCm);
            Assert.True(transportCm.State == State.Draft);
            Assert.Equal(transportAm.Id, transportCm.Id);
            Assert.Equal(transportAm.Name, transportCm.Name);
            Assert.Equal(transportAm.RdsName, transportCm.RdsName);
            Assert.Equal(transportAm.RdsCode, transportCm.RdsCode);
            Assert.Equal(transportAm.PurposeName, transportCm.PurposeName);
            Assert.Equal(transportAm.Description, transportCm.Description);
            Assert.Equal(transportAm.Aspect, transportCm.Aspect);
            Assert.Equal(transportAm.CompanyId, transportCm.CompanyId);
            Assert.Equal(transportAm.TerminalId, transportCm.TerminalId);
            Assert.Equal(transportAm.AttributeIdList.ToList().ConvertToString(), transportCm.Attributes.Select(x => x.Id).ToList().ConvertToString());
            Assert.Equal(transportAm.TypeReferences.First().Iri, transportCm.TypeReferences.First().Iri);
            Assert.Equal(transportAm.TypeReferences.First().Name, transportCm.TypeReferences.First().Name);
            Assert.Equal(transportAm.TypeReferences.First().Source, transportCm.TypeReferences.First().Source);

            Assert.Equal(transportAm.TypeReferences.First().Subs.First().Name, transportCm.TypeReferences.First().Subs.First().Name);
            Assert.Equal(transportAm.TypeReferences.First().Subs.First().Iri, transportCm.TypeReferences.First().Subs.First().Iri);
            Assert.Equal(transportAm.ParentId, transportCm.ParentId);
        }

        [Fact]
        public async Task GetLatestVersions_Transport_Result_Ok()
        {
            var transportService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<ITransportService>();
            var terminalService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<ITerminalService>();

            var terminalAm = new TerminalLibAm
            {
                Name = "Terminal107809990",
                Color = "#45678",
                CompanyId = 1
            };

            var terminalCm = await terminalService.Create(terminalAm);

            var transportAm = new TransportLibAm
            {
                Name = "Transport4",
                RdsName = "RdsName",
                RdsCode = "RdsCode",
                PurposeName = "PurposeName",
                Description = "Description1",
                Aspect = Aspect.NotSet,
                CompanyId = 1, TerminalId = terminalCm.Id
            };


            var transportCm = await transportService.Create(transportAm);

            transportAm.Description = "Description v1.1";

            var transportCmUpdated = await transportService.Update(transportAm);

            Assert.True(transportCm?.Description == "Description1");
            Assert.True(transportCm.Version == "1.0");
            Assert.True(transportCmUpdated?.Description == "Description v1.1");
            Assert.True(transportCmUpdated.Version == "1.1");
        }


        [Fact]
        public async Task Update_Transport__Ok()
        {
            var terminalService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<ITerminalService>();
            var transportService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<ITransportService>();

            var terminalAm = new TerminalLibAm
            {
                Name = "Terminal5108909990",
                Color = "#45678",
                CompanyId = 1
            };

            var terminalCm = await terminalService.Create(terminalAm);

            var transportAm = new TransportLibAm
            {
                Name = "Transport6",
                RdsName = "RdsName",
                RdsCode = "RdsCode",
                PurposeName = "PurposeName",
                Description = "Description1",
                Aspect = Aspect.NotSet,
                CompanyId = 1,
                TerminalId = terminalCm.Id
            };

            var cm = await transportService.Create(transportAm);
            transportAm.Description = "Description2";
            var cmUpdated = await transportService.Update(transportAm);

            Assert.True(cm.Description == "Description1" && cm.Version == "1.0");
            Assert.True(cmUpdated.Description == "Description2" && cmUpdated.Version == "1.1");
        }
    }
}