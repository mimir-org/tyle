using Microsoft.Extensions.DependencyInjection;
using Mimirorg.Setup;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Services.Contracts;
using Xunit;
// ReSharper disable InconsistentNaming

namespace Mimirorg.Integration.Tests.Services
{
    public class AttributeServiceTests : IntegrationTest
    {
        public AttributeServiceTests(ApiWebApplicationFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task DatumDataReceiveOk()
        {
            using var scope = Factory.Server.Services.CreateScope();
            var attributeService = scope.ServiceProvider.GetRequiredService<IAttributeService>();

            var rangeSpecifying = await attributeService.GetQuantityDatumRangeSpecifying();
            var regularitySpecified = await attributeService.GetQuantityDatumRegularitySpecified();
            var specifiedProvenance = await attributeService.GetQuantityDatumSpecifiedProvenance();
            var specifiedScope = await attributeService.GetQuantityDatumSpecifiedScope();

            Assert.True(rangeSpecifying != null);
            Assert.True(regularitySpecified != null);
            Assert.True(specifiedProvenance != null);
            Assert.True(specifiedScope != null);

            Assert.True(rangeSpecifying.Any());
            Assert.True(regularitySpecified.Any());
            Assert.True(specifiedProvenance.Any());
            Assert.True(specifiedScope.Any());
        }

        [Fact]
        public async Task Create_Attributes_Valid_Attributes_Created_Ok()
        {
            using var scope = Factory.Server.Services.CreateScope();
            var attributeService = scope.ServiceProvider.GetRequiredService<IAttributeService>();
            var unitService = scope.ServiceProvider.GetRequiredService<IUnitService>();
            var units = (await unitService.Get()).ToList();

            Assert.True(units != null);
            Assert.True(units.Any());

            var attributeAm = new AttributeLibAm
            {
                Name = "attribute1",
                Aspect = Aspect.Function,
                Discipline = Discipline.Electrical,
                Select = Select.MultiSelect,
                QuantityDatumRangeSpecifying = "Normal",
                QuantityDatumSpecifiedProvenance = "Calculated",
                QuantityDatumRegularitySpecified = "Absolute",
                QuantityDatumSpecifiedScope = "Design Datum",
                CompanyId = 1,
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
                SelectValues = new List<string> { "value1", "VALUE2", "value3" },
                UnitIdList = new List<string>
                {
                    units[0]?.Id
                }
            };

            var attributeCm = await attributeService.Create(attributeAm);

            Assert.NotNull(attributeCm);
            Assert.True(attributeCm.State == State.Draft);
            Assert.Equal(attributeAm.Id, attributeCm.Id);
            Assert.Equal(attributeAm.Name, attributeCm.Name);
            Assert.Equal(attributeAm.Aspect.ToString(), attributeCm.Aspect.ToString());
            Assert.Equal(attributeAm.Discipline.ToString(), attributeCm.Discipline.ToString());
            Assert.Equal(attributeAm.Select.ToString(), attributeCm.Select.ToString());
            Assert.Equal(attributeAm.QuantityDatumSpecifiedProvenance, attributeCm.QuantityDatumSpecifiedProvenance);
            Assert.Equal(attributeAm.QuantityDatumRegularitySpecified, attributeCm.QuantityDatumRegularitySpecified);
            Assert.Equal(attributeAm.QuantityDatumRangeSpecifying, attributeCm.QuantityDatumRangeSpecifying);
            Assert.Equal(attributeAm.QuantityDatumSpecifiedScope, attributeCm.QuantityDatumSpecifiedScope);
            Assert.Equal(attributeAm.CompanyId, attributeCm.CompanyId);

            Assert.Equal(attributeAm.TypeReferences.First().Iri, attributeCm.TypeReferences.First().Iri);
            Assert.Equal(attributeAm.TypeReferences.First().Name, attributeCm.TypeReferences.First().Name);
            Assert.Equal(attributeAm.TypeReferences.First().Source, attributeCm.TypeReferences.First().Source);

            Assert.Equal(attributeAm.TypeReferences.First().Subs.First().Name, attributeCm.TypeReferences.First().Subs.First().Name);
            Assert.Equal(attributeAm.TypeReferences.First().Subs.First().Iri, attributeCm.TypeReferences.First().Subs.First().Iri);

            var amSelectValues = attributeAm.SelectValues.OrderBy(x => x, StringComparer.InvariantCulture).ToList();
            var cmSelectValues = attributeCm.SelectValues.OrderBy(x => x, StringComparer.InvariantCulture).ToList();

            for (var i = 0; i < amSelectValues.Count; i++)
                Assert.Equal(amSelectValues[i], cmSelectValues[i]);

            var amUnitIdList = attributeAm.UnitIdList.OrderBy(x => x, StringComparer.InvariantCulture).ToList();
            var cmUnitIdList = attributeCm.Units.Select(x => x.Id).OrderBy(x => x, StringComparer.InvariantCulture).ToList();

            for (var i = 0; i < amUnitIdList.Count; i++)
                Assert.Equal(amUnitIdList[i], cmUnitIdList[i]);
        }

        [Fact]
        public async Task GetLatestVersions_Attribute_Result_Ok()
        {
            using var scope = Factory.Server.Services.CreateScope();

            var attributeService = scope.ServiceProvider.GetRequiredService<IAttributeService>();
            var unitService = scope.ServiceProvider.GetRequiredService<IUnitService>();
            var units = (await unitService.Get()).ToList();

            var attributeAm = new AttributeLibAm
            {
                Name = "attribute2",
                Aspect = Aspect.Function,
                Discipline = Discipline.Electrical,
                Select = Select.MultiSelect,
                SelectValues = new List<string> { "value1", "VALUE2", "value3" },
                QuantityDatumRangeSpecifying = "Normal",
                QuantityDatumSpecifiedProvenance = "Calculated",
                QuantityDatumRegularitySpecified = "Absolute",
                QuantityDatumSpecifiedScope = "Design Datum",
                CompanyId = 1,
                UnitIdList = new List<string>
                {
                    units[0]?.Id
                }
            };

            var attributeCm = await attributeService.Create(attributeAm);

            Assert.True(attributeCm.Version == "1.0");

            attributeAm.UnitIdList.Add(units[0]?.Id);
            var attributeCmUpdated = await attributeService.Update(attributeAm, attributeAm.Id);

            Assert.True(attributeCm.Version == "1.0");
            Assert.True(attributeCmUpdated.Version == "2.0");
        }

        [Fact]
        public async Task Delete_Attribute_Result_Ok()
        {
            using var scope = Factory.Server.Services.CreateScope();
            var attributeService = scope.ServiceProvider.GetRequiredService<IAttributeService>();

            var attributeAm = new AttributeLibAm
            {
                Name = "attribute3",
                Aspect = Aspect.Function,
                Discipline = Discipline.Electrical,
                Select = Select.MultiSelect,
                SelectValues = new List<string> { "value1", "VALUE2", "value3" },
                QuantityDatumRangeSpecifying = "Normal",
                QuantityDatumSpecifiedProvenance = "Calculated",
                QuantityDatumRegularitySpecified = "Absolute",
                QuantityDatumSpecifiedScope = "Design Datum",
                CompanyId = 1
            };

            var attributeCm = await attributeService.Create(attributeAm);

            var isDeleted = await attributeService.Delete(attributeCm?.Id);
            var allAttributesNotDeleted = attributeService.GetAll(Aspect.Function);
            var allAttributesIncludeDeleted = attributeService.GetAll(Aspect.Function, true);

            Assert.True(isDeleted);
            Assert.True(string.IsNullOrEmpty(allAttributesNotDeleted?.FirstOrDefault(x => x.Id == attributeCm?.Id)?.Id));
            Assert.True(!string.IsNullOrEmpty(allAttributesIncludeDeleted?.FirstOrDefault(x => x.Id == attributeCm?.Id)?.Id));
        }

        [Fact]
        public async Task Update_Attribute_State_Result_Ok()
        {
            using var scope = Factory.Server.Services.CreateScope();
            var attributeService = scope.ServiceProvider.GetRequiredService<IAttributeService>();

            var attributeAm = new AttributeLibAm
            {
                Name = "attribute4",
                Aspect = Aspect.Function,
                Discipline = Discipline.Electrical,
                Select = Select.MultiSelect,
                SelectValues = new List<string> { "value1", "VALUE2", "value3" },
                QuantityDatumRangeSpecifying = "Normal",
                QuantityDatumSpecifiedProvenance = "Calculated",
                QuantityDatumRegularitySpecified = "Absolute",
                QuantityDatumSpecifiedScope = "Design Datum",
                CompanyId = 1
            };

            var cm = await attributeService.Create(attributeAm);
            var cmUpdated = await attributeService.UpdateState(cm.Id, State.ApprovedCompany);

            Assert.True(cm.State != cmUpdated.State);
            Assert.True(cmUpdated.State == State.ApprovedCompany);
        }
    }
}