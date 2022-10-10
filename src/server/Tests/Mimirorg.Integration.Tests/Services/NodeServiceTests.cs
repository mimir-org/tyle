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
    public class NodeServiceTests : IntegrationTest
    {
        public NodeServiceTests(ApiWebApplicationFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task Create_Node_Returns_MimirorgDuplicateException_When_Already_Exist()
        {
            var nodeAm = new NodeLibAm
            {
                Name = "Node1",
                RdsName = "RdsName",
                RdsCode = "RdsCode",
                PurposeName = "PurposeName",
                Description = "Description",
                Aspect = Aspect.NotSet,
                CompanyId = 1,
                Version = "1.0"
            };

            var nodeService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<INodeService>();
            await nodeService.Create(nodeAm);
            Task Act() => nodeService.Create(nodeAm);
            _ = await Assert.ThrowsAsync<MimirorgDuplicateException>(Act);
        }

        [Fact]
        public async Task Create_Node_Create_Node_When_Ok_Parameters()
        {
            var nodeService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<INodeService>();
            var attributeService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<IAttributeService>();

            var attributeAm = new AttributeLibAm
            {
                Name = "attribute12345678",
                Aspect = Aspect.Function,
                Discipline = Discipline.Electrical,
                Select = Select.MultiSelect,
                Description = "Description1",
                SelectValues = new List<string> { "value1", "VALUE2", "value3" },
                QuantityDatumRangeSpecifying = "Normal",
                QuantityDatumSpecifiedProvenance = "Calculated",
                QuantityDatumRegularitySpecified = "Absolute",
                QuantityDatumSpecifiedScope = "Design Datum",
                CompanyId = 1,
                Version = "1.0"
            };

            var attributeCm = await attributeService.Create(attributeAm);

            var nodeAm = new NodeLibAm
            {
                Name = "Node2",
                RdsName = "RdsName",
                RdsCode = "RdsCode",
                PurposeName = "PurposeName",
                Description = "Description",
                Aspect = Aspect.NotSet,
                CompanyId = 1,
                AttributeIdList = new List<string> { $"{attributeCm.Id}" },
                NodeTerminals = new List<NodeTerminalLibAm>{
                    new()
                    {
                        TerminalId = "8EBC5811473E87602FB0C18A100BD53C",
                        Quantity = 1,
                        ConnectorDirection = ConnectorDirection.Output
                    }
                },
                SelectedAttributePredefined = new List<SelectedAttributePredefinedLibAm>{
                    new()
                    {
                        Key = "1234",
                        IsMultiSelect = true,
                        Values = new Dictionary<string, bool>
                        {
                            {"56789", true}
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
                        }
                    }
                },
                Symbol = "symbol",
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
                ParentId = "1234",
                Version = "1.0"
            };

            var nodeCm = await nodeService.Create(nodeAm);

            Assert.NotNull(nodeCm);
            Assert.True(nodeCm.State == State.Draft);
            Assert.Equal(nodeAm.Id, nodeCm.Id);
            Assert.Equal(nodeAm.Name, nodeCm.Name);
            Assert.Equal(nodeAm.RdsName, nodeCm.RdsName);
            Assert.Equal(nodeAm.RdsCode, nodeCm.RdsCode);
            Assert.Equal(nodeAm.PurposeName, nodeCm.PurposeName);
            Assert.Equal(nodeAm.Aspect, nodeCm.Aspect);
            Assert.Equal(nodeAm.Description, nodeCm.Description);
            Assert.Equal(nodeAm.CompanyId, nodeCm.CompanyId);
            Assert.Equal(nodeAm.AttributeIdList.ToList().ConvertToString(), nodeCm.Attributes.Select(x => x.Id).ToList().ConvertToString());

            foreach (var am in nodeAm.NodeTerminals)
            {
                foreach (var cm in nodeCm.NodeTerminals)
                {
                    Assert.Equal(am.TerminalId, cm.Terminal.Id);
                    Assert.Equal(am.Quantity, cm.Quantity);
                    Assert.Equal(am.ConnectorDirection.ToString(), cm.ConnectorDirection.ToString());
                }
            }

            Assert.Equal(nodeAm.SelectedAttributePredefined.First().Key, nodeCm.SelectedAttributePredefined.First().Key);
            Assert.Equal(nodeAm.SelectedAttributePredefined.First().IsMultiSelect, nodeCm.SelectedAttributePredefined.First().IsMultiSelect);
            Assert.Equal(nodeAm.SelectedAttributePredefined.First().Values.ToString(), nodeCm.SelectedAttributePredefined.First().Values.ToString());

            Assert.Equal(nodeAm.SelectedAttributePredefined.First().TypeReferences.First().Iri, nodeCm.SelectedAttributePredefined.First().TypeReferences.First().Iri);
            Assert.Equal(nodeAm.SelectedAttributePredefined.First().TypeReferences.First().Name, nodeCm.SelectedAttributePredefined.First().TypeReferences.First().Name);
            Assert.Equal(nodeAm.SelectedAttributePredefined.First().TypeReferences.First().Source, nodeCm.SelectedAttributePredefined.First().TypeReferences.First().Source);
            Assert.Equal(nodeAm.SelectedAttributePredefined.First().TypeReferences.First().Subs.First().Name, nodeCm.SelectedAttributePredefined.First().TypeReferences.First().Subs.First().Name);
            Assert.Equal(nodeAm.SelectedAttributePredefined.First().TypeReferences.First().Subs.First().Iri, nodeCm.SelectedAttributePredefined.First().TypeReferences.First().Subs.First().Iri);

            Assert.Equal(nodeAm.TypeReferences.First().Iri, nodeCm.TypeReferences.First().Iri);
            Assert.Equal(nodeAm.TypeReferences.First().Name, nodeCm.TypeReferences.First().Name);
            Assert.Equal(nodeAm.TypeReferences.First().Source, nodeCm.TypeReferences.First().Source);
            Assert.Equal(nodeAm.TypeReferences.First().Subs.First().Name, nodeCm.TypeReferences.First().Subs.First().Name);
            Assert.Equal(nodeAm.TypeReferences.First().Subs.First().Iri, nodeCm.TypeReferences.First().Subs.First().Iri);

            Assert.Equal(nodeAm.Symbol, nodeCm.Symbol);
            Assert.Equal(nodeAm.ParentId, nodeCm.ParentId);
        }

        [Fact]
        public async Task GetLatestVersions_Nodes_Result_Ok()
        {
            var nodeAm = new NodeLibAm
            {
                Name = "Node4",
                RdsName = "RdsName",
                RdsCode = "RdsCode",
                PurposeName = "PurposeName",
                Description = "Description",
                Aspect = Aspect.NotSet,
                CompanyId = 1,
                Version = "1.0"
            };

            var nodeService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<INodeService>();
            var nodeCm = await nodeService.Create(nodeAm);

            nodeAm.Description = "Description v1.1";

            var nodeCmUpdated = await nodeService.Update(nodeAm);

            Assert.True(nodeCm?.Description == "Description");
            Assert.True(nodeCm.Version == "1.0");
            Assert.True(nodeCmUpdated?.Description == "Description v1.1");
            Assert.True(nodeCmUpdated.Version == "1.1");
        }


        [Fact]
        public async Task Update_Node_Result_Ok()
        {
            var nodeAm = new NodeLibAm
            {
                Name = "Node6",
                RdsName = "RdsName",
                RdsCode = "RdsCode",
                PurposeName = "PurposeName",
                Description = "Description1",
                Aspect = Aspect.NotSet,
                CompanyId = 1,
                Version = "1.0"
            };

            var nodeService = Factory.Server.Services.CreateScope().ServiceProvider.GetRequiredService<INodeService>();

            var cm = await nodeService.Create(nodeAm);
            nodeAm.Description = "Description2";
            var cmUpdated = await nodeService.Update(nodeAm);

            Assert.True(cm.Description == "Description1" && cm.Version == "1.0");
            Assert.True(cmUpdated.Description == "Description2" && cmUpdated.Version == "1.1");
        }
    }
}