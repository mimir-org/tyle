using Mimirorg.Common.Attributes;
using Mimirorg.Common.Extensions;
using Xunit;

namespace Mimirorg.Unit.Tests.Attributes
{
    public class ValidationAttributeTests
    {
        [Theory]
        [InlineData("https://rdf.runir.net/ID1234", true)]
        [InlineData("https://rdf.runir.net/", false)]
        [InlineData(null, true)]
        [InlineData("", true)]
        [InlineData("https://rdf.runir.net/ID1234/123/123", true)]
        [InlineData("rdf.runir.net/ID1234", false)]
        public void Iri_Attribute_Validates_Correctly(string value, bool result)
        {
            var attribute = new ValidIriAttribute();
            var isValid = attribute.IsValid(value);
            Assert.Equal(result, isValid);
        }

        [Theory]
        [InlineData("1234", "1234", true)]
        [InlineData("", "", false)]
        [InlineData(null, null, false)]
        [InlineData("", "1234", true)]
        [InlineData(null, "1234", true)]
        [InlineData("1234", "", true)]
        [InlineData("1234", null, true)]
        public void RequiredOne_Attribute_Validates_Correctly(string value, string dependent, bool result)
        {
            var model = new RequiredOneTestValidator { Id = value, Iri = dependent };
            var validation = model.ValidateObject();
            Assert.Equal(result, validation.IsValid);
        }
    }

    internal class RequiredOneTestValidator
    {
        [RequiredOne("Iri")]
        public string Id { get; set; }

        [RequiredOne("Id")]
        public string Iri { get; set; }
    }
}