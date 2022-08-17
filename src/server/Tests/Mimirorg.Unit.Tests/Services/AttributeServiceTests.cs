using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Mimirorg.Common.Exceptions;
using Mimirorg.Setup;
using Mimirorg.Setup.Fixtures;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using TypeLibrary.Services.Services;
using Xunit;

namespace Mimirorg.Unit.Tests.Services
{
    public class AttributeServiceTests : UnitTest<MimirorgCommonFixture>
    {

        private readonly AttributeService _attributeService;

        public AttributeServiceTests(MimirorgCommonFixture fixture) : base(fixture)
        {
            _attributeService = new AttributeService(
                fixture.Mapper.Object,
                fixture.AttributeRepository.Object,
                Options.Create(fixture.ApplicationSettings),
                fixture.AttributeQualifierRepository.Object,
                fixture.AttributeSourceRepository.Object,
                fixture.AttributeFormatRepository.Object,
                fixture.AttributeConditionRepository.Object,
                fixture.AttributePredefinedRepository.Object);
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

            //Act
            Task Act() => _attributeService.Create(new List<AttributeLibAm> { attribute });

            //Assert
            _ = await Assert.ThrowsAsync<MimirorgBadRequestException>(Act);
        }
    }
}