using Mimirorg.Common.Extensions;
using Mimirorg.Setup.Tests;
using Xunit;

// ReSharper disable StringLiteralTypo

namespace Mimirorg.Unit.Tests.Extensions
{
    public class StringExtensionTests : IClassFixture<MimirorgCommonFixture>
    {
        [Fact]
        public void ResolveNormalizedName_Returns_Correct_Values()
        {
            const string name = "Account_Manager % - _123";
            var result = name.ResolveNormalizedName();
            Assert.Equal("ACCOUNTMANAGER123", result);
        }
    }
}