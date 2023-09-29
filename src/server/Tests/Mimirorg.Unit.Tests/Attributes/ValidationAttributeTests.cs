using Mimirorg.Authentication.Attributes;
using Mimirorg.Authentication.Extensions;
using Mimirorg.Authentication.Models;
using Mimirorg.Test.Setup;
using Mimirorg.Test.Setup.Fixtures;
using TypeLibrary.Services.Common.Requests;
using Xunit;

namespace Mimirorg.Test.Unit.Attributes;

public class ValidationAttributeTests : UnitTest<MimirorgCommonFixture>
{
    private readonly MimirorgCommonFixture _fixture;

    public ValidationAttributeTests(MimirorgCommonFixture fixture) : base(fixture)
    {
        _fixture = fixture;
    }

    [Theory]
    [InlineData("https://rdf.runir.net/ID1234", true)]
    [InlineData("https://rdf.runir.net/", true)]
    [InlineData(null, true)]
    [InlineData("", false)]
    [InlineData("https://rdf.runir.net/ID1234/123/123", true)]
    [InlineData("rdf.runir.net/ID1234", false)]
    public void Iri_Attribute_Validates_Correctly(string value, bool result)
    {
        var attribute = new ValidIriAttribute();
        var isValid = attribute.IsValid(value);
        Assert.Equal(result, isValid);
    }

    [Fact]
    public void Iri_Attribute_Fails_On_Non_String()
    {
        var attribute = new ValidIriAttribute();
        var isValid = attribute.IsValid(12);
        Assert.False(isValid);
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

    [Theory]
    [InlineData("1234", false)]
    [InlineData("Passw0rd123!", true)]
    [InlineData("Passw0rd123", false)]
    [InlineData("passw0rd123!", false)]
    [InlineData("passwKrdHHH!", false)]
    [InlineData("Passw0rd1234", false)]
    public void PasswordAttribute_Validates_Correctly(string value, bool result)
    {
        var validation = value.HasValidPassword(_fixture.MimirorgAuthSettings);
        var isValid = validation?.ErrorMessage == null || validation.ErrorMessage?.Length <= 0;
        Assert.Equal(result, isValid);
    }

    [Theory]
    [InlineData("hhh", true)]
    [InlineData("123", true)]
    [InlineData("1", true)]
    [InlineData("d", true)]
    [InlineData("", false)]
    [InlineData(" ", false)]
    [InlineData(null, false)]
    public void PasswordAttribute_Validates_Correctly_With_No_Rules(string value, bool result)
    {
        var settings = new MimirorgAuthSettings
        {
            RequireDigit = false,
            RequireNonAlphanumeric = false,
            RequireUppercase = false,
            RequiredLength = 0,
        };
        var validation = value.HasValidPassword(settings);
        var isValid = validation?.ErrorMessage == null || validation.ErrorMessage?.Length <= 0;
        Assert.Equal(result, isValid);
    }
}

internal class RequiredOneTestValidator
{
    [RequiredOne("Iri")]
    public string Id { get; set; }

    [RequiredOne("Id")]
    public string Iri { get; set; }
}