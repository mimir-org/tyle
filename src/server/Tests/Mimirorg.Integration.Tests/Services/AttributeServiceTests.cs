using Microsoft.Extensions.DependencyInjection;
using Mimirorg.Common.Enums;
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
        public async Task Get_Latest_version_Attribute_Returns_Correct_Version()
        {
            using var scope = Factory.Server.Services.CreateScope();
            var attributeService = scope.ServiceProvider.GetRequiredService<IAttributeService>();
            var unitService = scope.ServiceProvider.GetRequiredService<IUnitService>();
            var units = (await unitService.Get()).ToList();

            var attribute = new AttributeLibAm
            {
                Name = "First_Version_Attribute_XXX",
                Aspect = Aspect.Function,
                Discipline = Discipline.Process,
                Select = Select.None,
                Description = "This is test a",
                QuantityDatumRangeSpecifying = "Actual Datum",
                QuantityDatumRegularitySpecified = "Absolute Datum",
                QuantityDatumSpecifiedProvenance = "Calculated Datum",
                QuantityDatumSpecifiedScope = "Design Datum",
                CompanyId = 1,
                UnitIdList = null,
                Version = "1.0"
            };

            var createdAttribute = await attributeService.Create(attribute);
            attribute.Description = "This is test b";

            var updatedAttribute = await attributeService.Update(attribute);
            attribute.UnitIdList = new List<string> { units.FirstOrDefault()?.Id };

            var updatedAttribute2 = await attributeService.Update(attribute);
            attribute.Description = "This is test c";

            var updatedAttribute3 = await attributeService.Update(attribute);

            Assert.True(createdAttribute.Version == "1.0");
            Assert.True(updatedAttribute.Version == "1.1");
            Assert.True(updatedAttribute2.Version == "2.0");
            Assert.True(updatedAttribute3.Version == "2.1");

            var allObjects = attributeService.GetLatestVersions(Aspect.NotSet);
            var actualObjects = allObjects.Where(x => x.Name == "First_Version_Attribute_XXX").ToList();

            Assert.NotNull(actualObjects);
            Assert.True(actualObjects.Count == 1);
            Assert.True(actualObjects.FirstOrDefault()?.Version == "2.1");
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
            var attributeCmUpdated = await attributeService.Update(attributeAm);

            Assert.True(attributeCm.Version == "1.0");
            Assert.True(attributeCmUpdated.Version == "2.0");
        }

        [Fact]
        public async Task Update_Attribute_Result_Ok()
        {
            using var scope = Factory.Server.Services.CreateScope();
            var attributeService = scope.ServiceProvider.GetRequiredService<IAttributeService>();

            var attributeAm = new AttributeLibAm
            {
                Name = "attribute4",
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

            var cm = await attributeService.Create(attributeAm);
            attributeAm.Description = "Description2";
            var cmUpdated = await attributeService.Update(attributeAm);

            Assert.True(cm.Description == "Description1" && cm.Version == "1.0");
            Assert.True(cmUpdated.Description == "Description2" && cmUpdated.Version == "1.1");
        }
    }
}