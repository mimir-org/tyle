using Microsoft.Extensions.DependencyInjection;
using Mimirorg.Common.Exceptions;
using Mimirorg.Setup.Tests;
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

        [Theory]
        [InlineData(null, Aspect.None, Discipline.NotSet, Select.None, "xxx", "xxx", "xxx", "xxx")]
        [InlineData("xxx", Aspect.None, Discipline.NotSet, Select.None, null, "xxx", "xxx", "xxx")]
        [InlineData("xxx", Aspect.None, Discipline.NotSet, Select.None, "xxx", null, "xxx", "xxx")]
        [InlineData("xxx", Aspect.None, Discipline.NotSet, Select.None, "xxx", "xxx", null, "xxx")]
        [InlineData("xxx", Aspect.None, Discipline.NotSet, Select.None, "xxx", "xxx", "xxx", null)]
        [InlineData("", Aspect.None, Discipline.NotSet, Select.None, "xxx", "xxx", "xxx", "xxx")]
        [InlineData("xxx", Aspect.None, Discipline.NotSet, Select.None, "", "xxx", "xxx", "xxx")]
        [InlineData("xxx", Aspect.None, Discipline.NotSet, Select.None, "xxx", "", "xxx", "xxx")]
        [InlineData("xxx", Aspect.None, Discipline.NotSet, Select.None, "xxx", "xxx", "", "xxx")]
        [InlineData("xxx", Aspect.None, Discipline.NotSet, Select.None, "xxx", "xxx", "xxx", "")]
        public async Task Create_Attributes_Throws_Bad_Request_When_Not_Valid(string name, Aspect aspect, Discipline discipline, Select select, string qualifier, string source, string condition, string format)
        {
            var attribute = new AttributeLibAm
            {
                Name = name,
                Aspect = aspect,
                Discipline = discipline,
                Select = select,
                AttributeQualifier = qualifier,
                AttributeSource = source,
                AttributeCondition = condition,
                AttributeFormat = format
            };

            using var scope = Factory.Server.Services.CreateScope();
            var attributeService = scope.ServiceProvider.GetRequiredService<IAttributeService>();

            //Act
            Task Act() => attributeService.Create(new List<AttributeLibAm> { attribute });

            //Assert
            _ = await Assert.ThrowsAsync<MimirorgBadRequestException>(Act);
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
                AttributeFormat = "f1"
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
                AttributeFormat = "f2"
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