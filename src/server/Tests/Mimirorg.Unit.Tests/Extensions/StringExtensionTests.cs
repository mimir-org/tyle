using System.Collections.Generic;
using Mimirorg.Common.Extensions;
using Mimirorg.Setup;
using Mimirorg.Setup.Fixtures;
using Xunit;

// ReSharper disable StringLiteralTypo

namespace Mimirorg.Unit.Tests.Extensions
{
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
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void HasEmptyValues_Returns_Correct_Value_When_EmptyOrNull_Strings(string value)
        {
            var list = new List<string> { value };
            var result = list.HasEmptyValues();
            Assert.True(result);
        }

        [Theory]
        [InlineData("Mimir", "Tyle")]
        public void HasEmptyValues_Returns_Correct_Value_When_Not_EmptyOrNull_Strings(string value1, string value2)
        {
            var list = new List<string> { value1, value2 };
            var result = list.HasEmptyValues();
            Assert.True(result);
        }

        [Theory]
        [InlineData("Mimir", "Tyle", " ")]
        public void HasEmptyValues_Returns_Correct_Value_When_Combined_Strings(string value1, string value2, string value3)
        {
            var list = new List<string> { value1, value2, value3 };
            var result = list.HasEmptyValues();
            Assert.True(result);
        }
    }
}