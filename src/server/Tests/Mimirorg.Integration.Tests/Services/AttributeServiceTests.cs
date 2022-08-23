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
            var attribute1 = new AttributeLibAm
            {
                Name = "xxx_created_1",
                Aspect = Aspect.Function,
                Discipline = Discipline.None,
                Select = Select.None,
                AttributeQualifier = "q1",
                AttributeSource = "s1",
                AttributeCondition = "c1",
                AttributeFormat = "f1",
                CompanyId = 1,
                AttributeType = AttributeType.Normal
            };

            var attribute2 = new AttributeLibAm
            {
                Name = "xxx_created_2",
                Aspect = Aspect.Function,
                Discipline = Discipline.None,
                Select = Select.None,
                AttributeQualifier = "q2",
                AttributeSource = "s2",
                AttributeCondition = "c2",
                AttributeFormat = "f2",
                CompanyId = 1,
                AttributeType = AttributeType.Normal
            };

            using var scope = Factory.Server.Services.CreateScope();
            var attributeService = scope.ServiceProvider.GetRequiredService<IAttributeService>();

            //Act
            await attributeService.Create(new List<AttributeLibAm> { attribute1, attribute2 });

            //Assert
            var attributes = attributeService.Get(Aspect.NotSet).Where(x => x.Name == "xxx_created_1" || x.Name == "xxx_created_2").ToList();
            Assert.NotNull(attributes);
            Assert.Equal(2, attributes.Count);
        }
    }
}