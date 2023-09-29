using Mimirorg.Authentication.Extensions;
using Mimirorg.Test.Setup;
using Mimirorg.Test.Setup.Fixtures;
using Xunit;

// ReSharper disable StringLiteralTypo

namespace Mimirorg.Test.Unit.Extensions;

public class StringExtensionTests : UnitTest<MimirorgCommonFixture>
{
    public StringExtensionTests(MimirorgCommonFixture fixture) : base(fixture)
    {
    }

    [Fact]
    public void ResolveNormalizedName_Returns_Correct_Values()
    {
        const string name = "Account_Manager % - _123";
        var result = name.ResolveNormalizedName();
        Assert.Equal("ACCOUNTMANAGER123", result);
    }

    [Theory]
    [InlineData("99b212bf-013b-4b47-8ee7-192ba76ef5bd")]
    public void CreateSha512_Returns_Correct_Value(string value)
    {
        var result = value.CreateSha512();
        Assert.True(result == "96E479000CCE97488437A7E501EE171859373E273A1789133367D0B36520A6AA4592619A0E540291BDB70B9A27BC1B421D5F96B8BE2A92682F3C98B822C0E2B4");
    }
}