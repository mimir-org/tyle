using Microsoft.Extensions.DependencyInjection;
using Mimirorg.Setup;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using TypeLibrary.Services.Contracts;
using Xunit;

namespace Mimirorg.Integration.Tests.Services
{
    public class AttributeServiceTests : IntegrationTest
    {
        public AttributeServiceTests(ApiWebApplicationFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task Create_Attributes_Valid_Attributes_Created_Ok()
        {
            using var scope = Factory.Server.Services.CreateScope();

            var unitService = scope.ServiceProvider.GetRequiredService<IUnitService>();
            var attributeService = scope.ServiceProvider.GetRequiredService<IAttributeService>();

            var qualifiers = await attributeService.GetQualifiers();
            var sources = await attributeService.GetSources();
            var conditions = await attributeService.GetConditions();
            var formats = await attributeService.GetFormats();
            var units = await unitService.Get();

            Assert.True(qualifiers != null);
            Assert.True(sources != null);
            Assert.True(conditions != null);
            Assert.True(formats != null);
            Assert.True(units != null);

            var qualifierCmList = qualifiers.ToList();
            var sourceCmList = sources.ToList();
            var conditionCmList = conditions.ToList();
            var formatCmList = formats.ToList();
            var unitCmList = units.ToList();

            Assert.True(qualifierCmList.Any() && qualifierCmList.Count > 1);
            Assert.True(sourceCmList.Any() && sourceCmList.Count > 1);
            Assert.True(conditionCmList.Any() && conditionCmList.Count > 1);
            Assert.True(formatCmList.Any() && formatCmList.Count > 1);
            Assert.True(unitCmList.Any() && unitCmList.Count > 2);

            var attributeAm = new AttributeLibAm
            {
                Name = "attribute1",
                Aspect = Aspect.Function,
                Discipline = Discipline.Electrical,
                Select = Select.MultiSelect,
                AttributeQualifier = qualifierCmList[1]?.Name,
                AttributeSource = sourceCmList[1]?.Name,
                AttributeCondition = conditionCmList[1]?.Name,
                AttributeFormat = formatCmList[1]?.Name,
                CompanyId = 1,
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
                SelectValues = new List<string> { "value1", "VALUE2", "value3" },
                UnitIdList = new List<string>
                {
                    unitCmList[0]?.Id,
                    unitCmList[1]?.Id,
                    unitCmList[2]?.Id
                },
                Tags = new HashSet<string> { "set1", "set2" }
            };

            var attributeCm = await attributeService.Create(attributeAm);

            Assert.NotNull(attributeCm);
            Assert.True(attributeCm.State == State.Draft);
            Assert.Equal(attributeAm.Id, attributeCm.Id);
            Assert.Equal(attributeAm.Name, attributeCm.Name);
            Assert.Equal(attributeAm.Aspect.ToString(), attributeCm.Aspect.ToString());
            Assert.Equal(attributeAm.Discipline.ToString(), attributeCm.Discipline.ToString());
            Assert.Equal(attributeAm.Select.ToString(), attributeCm.Select.ToString());
            Assert.Equal(attributeAm.AttributeQualifier, attributeCm.AttributeQualifier);
            Assert.Equal(attributeAm.AttributeSource, attributeCm.AttributeSource);
            Assert.Equal(attributeAm.AttributeCondition, attributeCm.AttributeCondition);
            Assert.Equal(attributeAm.AttributeFormat, attributeCm.AttributeFormat);
            Assert.Equal(attributeAm.CompanyId, attributeCm.CompanyId);

            Assert.Equal(attributeAm.TypeReferences.First().Iri, attributeCm.TypeReferences.First().Iri);
            Assert.Equal(attributeAm.TypeReferences.First().Name, attributeCm.TypeReferences.First().Name);
            Assert.Equal(attributeAm.TypeReferences.First().Source, attributeCm.TypeReferences.First().Source);
            Assert.Equal(attributeAm.TypeReferences.First().SubIri, attributeCm.TypeReferences.First().SubIri);
            Assert.Equal(attributeAm.TypeReferences.First().SubName, attributeCm.TypeReferences.First().SubName);

            var amSelectValues = attributeAm.SelectValues.OrderBy(x => x, StringComparer.InvariantCulture).ToList();
            var cmSelectValues = attributeCm.SelectValues.OrderBy(x => x, StringComparer.InvariantCulture).ToList();

            for (var i = 0; i < amSelectValues.Count; i++)
                Assert.Equal(amSelectValues[i], cmSelectValues[i]);

            var amUnitIdList = attributeAm.UnitIdList.OrderBy(x => x, StringComparer.InvariantCulture).ToList();
            var cmUnitIdList = attributeCm.Units.Select(x => x.Id).OrderBy(x => x, StringComparer.InvariantCulture).ToList();

            for (var i = 0; i < amUnitIdList.Count; i++)
                Assert.Equal(amUnitIdList[i], cmUnitIdList[i]);

            var amTagList = attributeAm.Tags.OrderBy(x => x, StringComparer.InvariantCulture).ToList();
            var cmTagList = attributeCm.Tags.OrderBy(x => x, StringComparer.InvariantCulture).ToList();

            for (var i = 0; i < amTagList.Count; i++)
                Assert.Equal(amTagList[i], cmTagList[i]);
        }

        [Fact]
        public async Task GetLatestVersions_Attribute_Result_Ok()
        {
            using var scope = Factory.Server.Services.CreateScope();

            var unitService = scope.ServiceProvider.GetRequiredService<IUnitService>();
            var attributeService = scope.ServiceProvider.GetRequiredService<IAttributeService>();

            var qualifiers = await attributeService.GetQualifiers();
            var sources = await attributeService.GetSources();
            var conditions = await attributeService.GetConditions();
            var formats = await attributeService.GetFormats();
            var units = await unitService.Get();

            Assert.True(qualifiers != null);
            Assert.True(sources != null);
            Assert.True(conditions != null);
            Assert.True(formats != null);
            Assert.True(units != null);

            var qualifierCmList = qualifiers.ToList();
            var sourceCmList = sources.ToList();
            var conditionCmList = conditions.ToList();
            var formatCmList = formats.ToList();
            var unitCmList = units.ToList();

            Assert.True(qualifierCmList.Any() && qualifierCmList.Count > 1);
            Assert.True(sourceCmList.Any() && sourceCmList.Count > 1);
            Assert.True(conditionCmList.Any() && conditionCmList.Count > 1);
            Assert.True(formatCmList.Any() && formatCmList.Count > 1);
            Assert.True(unitCmList.Any() && unitCmList.Count > 3);

            var attributeAm = new AttributeLibAm
            {
                Name = "attribute2",
                Aspect = Aspect.Function,
                Discipline = Discipline.Electrical,
                Select = Select.MultiSelect,
                SelectValues = new List<string> { "value1", "VALUE2", "value3" },
                AttributeQualifier = qualifierCmList[1]?.Name,
                AttributeSource = sourceCmList[1]?.Name,
                AttributeCondition = conditionCmList[1]?.Name,
                AttributeFormat = formatCmList[1]?.Name,
                CompanyId = 1,
                UnitIdList = new List<string>
                {
                    unitCmList[0]?.Id,
                    unitCmList[1]?.Id,
                    unitCmList[2]?.Id
                }
            };

            var attributeCm = await attributeService.Create(attributeAm);

            Assert.True(attributeCm.Version == "1.0");

            attributeAm.UnitIdList.Add(unitCmList[3]?.Id);
            var attributeCmUpdated = await attributeService.Update(attributeAm, attributeAm.Id);

            Assert.True(attributeCm.Version == "1.0");
            Assert.True(attributeCmUpdated.Version == "2.0");
        }

        [Fact]
        public async Task Delete_Attribute_Result_Ok()
        {
            using var scope = Factory.Server.Services.CreateScope();
            var attributeService = scope.ServiceProvider.GetRequiredService<IAttributeService>();

            var qualifiers = await attributeService.GetQualifiers();
            var sources = await attributeService.GetSources();
            var conditions = await attributeService.GetConditions();
            var formats = await attributeService.GetFormats();

            Assert.True(qualifiers != null);
            Assert.True(sources != null);
            Assert.True(conditions != null);
            Assert.True(formats != null);

            var qualifierCmList = qualifiers.ToList();
            var sourceCmList = sources.ToList();
            var conditionCmList = conditions.ToList();
            var formatCmList = formats.ToList();

            Assert.True(qualifierCmList.Any() && qualifierCmList.Count > 1);
            Assert.True(sourceCmList.Any() && sourceCmList.Count > 1);
            Assert.True(conditionCmList.Any() && conditionCmList.Count > 1);
            Assert.True(formatCmList.Any() && formatCmList.Count > 1);

            var attributeAm = new AttributeLibAm
            {
                Name = "attribute3",
                Aspect = Aspect.Function,
                Discipline = Discipline.Electrical,
                Select = Select.MultiSelect,
                SelectValues = new List<string> { "value1", "VALUE2", "value3" },
                AttributeQualifier = qualifierCmList[1]?.Name,
                AttributeSource = sourceCmList[1]?.Name,
                AttributeCondition = conditionCmList[1]?.Name,
                AttributeFormat = formatCmList[1]?.Name,
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

            var qualifiers = await attributeService.GetQualifiers();
            var sources = await attributeService.GetSources();
            var conditions = await attributeService.GetConditions();
            var formats = await attributeService.GetFormats();

            Assert.True(qualifiers != null);
            Assert.True(sources != null);
            Assert.True(conditions != null);
            Assert.True(formats != null);

            var qualifierCmList = qualifiers.ToList();
            var sourceCmList = sources.ToList();
            var conditionCmList = conditions.ToList();
            var formatCmList = formats.ToList();

            Assert.True(qualifierCmList.Any() && qualifierCmList.Count > 1);
            Assert.True(sourceCmList.Any() && sourceCmList.Count > 1);
            Assert.True(conditionCmList.Any() && conditionCmList.Count > 1);
            Assert.True(formatCmList.Any() && formatCmList.Count > 1);

            var attributeAm = new AttributeLibAm
            {
                Name = "attribute4",
                Aspect = Aspect.Function,
                Discipline = Discipline.Electrical,
                Select = Select.MultiSelect,
                SelectValues = new List<string> { "value1", "VALUE2", "value3" },
                AttributeQualifier = qualifierCmList[1]?.Name,
                AttributeSource = sourceCmList[1]?.Name,
                AttributeCondition = conditionCmList[1]?.Name,
                AttributeFormat = formatCmList[1]?.Name,
                CompanyId = 1
            };

            var cm = await attributeService.Create(attributeAm);
            var cmUpdated = await attributeService.UpdateState(cm.Id, State.ApprovedCompany);

            Assert.True(cm.State != cmUpdated.State);
            Assert.True(cmUpdated.State == State.ApprovedCompany);
        }
    }
}