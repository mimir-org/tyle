using Microsoft.Extensions.Options;
using Mimirorg.Test.Setup;
using Mimirorg.Test.Setup.Fixtures;
using TypeLibrary.Services.Services;

// ReSharper disable InconsistentNaming

namespace Mimirorg.Test.Unit.Services
{
    public class AttributeServiceTests : UnitTest<MimirorgCommonFixture>
    {

        private readonly AttributeService _attributeService;

        public AttributeServiceTests(MimirorgCommonFixture fixture) : base(fixture)
        {
            _attributeService = new AttributeService(
                fixture.Mapper.Object,
                Options.Create(fixture.ApplicationSettings),
                fixture.AttributePredefinedRepository.Object,
                fixture.AttributeReferenceRepository.Object,
                fixture.DatumRepository.Object);
        }

        //[Theory]
        //[InlineData(null, Aspect.None, Discipline.NotSet, Select.None, "xxx", "xxx", "xxx", "xxx")]
        //[InlineData("xxx", Aspect.None, Discipline.NotSet, Select.None, null, "xxx", "xxx", "xxx")]
        //[InlineData("xxx", Aspect.None, Discipline.NotSet, Select.None, "xxx", null, "xxx", "xxx")]
        //[InlineData("xxx", Aspect.None, Discipline.NotSet, Select.None, "xxx", "xxx", null, "xxx")]
        //[InlineData("xxx", Aspect.None, Discipline.NotSet, Select.None, "xxx", "xxx", "xxx", null)]
        //[InlineData("", Aspect.None, Discipline.NotSet, Select.None, "xxx", "xxx", "xxx", "xxx")]
        //[InlineData("xxx", Aspect.None, Discipline.NotSet, Select.None, "", "xxx", "xxx", "xxx")]
        //[InlineData("xxx", Aspect.None, Discipline.NotSet, Select.None, "xxx", "", "xxx", "xxx")]
        //[InlineData("xxx", Aspect.None, Discipline.NotSet, Select.None, "xxx", "xxx", "", "xxx")]
        //[InlineData("xxx", Aspect.None, Discipline.NotSet, Select.None, "xxx", "xxx", "xxx", "")]
        //public async Task Create_Attributes_Throws_Bad_Request_When_Not_Valid(string name, Aspect aspect, Discipline discipline, Select select, string quantityDatumSpecifiedScope, string quantityDatumSpecifiedProvenance, string quantityDatumRangeSpecifying, string quantityDatumRegularitySpecified)
        //{
        //    var attribute = new AttributeLibAm
        //    {
        //        Name = name,
        //        Aspect = aspect,
        //        Discipline = discipline,
        //        Select = select,
        //        QuantityDatumSpecifiedScope = quantityDatumSpecifiedScope,
        //        QuantityDatumSpecifiedProvenance = quantityDatumSpecifiedProvenance,
        //        QuantityDatumRangeSpecifying = quantityDatumRangeSpecifying,
        //        QuantityDatumRegularitySpecified = quantityDatumRegularitySpecified
        //    };

        //    //Act
        //    Task Act() => _attributeService.Create(attribute);

        //    //Assert
        //    _ = await Assert.ThrowsAsync<MimirorgBadRequestException>(Act);
        //}
    }
}