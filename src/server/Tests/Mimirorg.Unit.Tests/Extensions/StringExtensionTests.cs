using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper.Execution;
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
            Assert.False(result);
        }

        [Theory]
        [InlineData("Mimir", "Tyle", " ")]
        public void HasEmptyValues_Returns_Correct_Value_When_Combined_Strings(string value1, string value2, string value3)
        {
            var list = new List<string> { value1, value2, value3 };
            var result = list.HasEmptyValues();
            Assert.True(result);
        }

        [Theory]
        [InlineData("Mimir, Tyle,Runir ")]
        public void ConvertToArray_Returns_Correct_Array_When_Multiple_Values(string value)
        {
            var result = value.ConvertToArray();
            Assert.True(result.Count == 3);
            Assert.True(result.Contains("Mimir"));
            Assert.True(result.Contains("Tyle"));
            Assert.True(result.Contains("Runir"));
            Assert.False(result.Contains(","));
            Assert.False(result.Contains("Runir "));
            Assert.False(result.Contains(" Runir"));
        }

        [Theory]
        [InlineData("Mimir")]
        public void ConvertToArray_Returns_Correct_Array_When_One_Value(string value)
        {
            var result = value.ConvertToArray();
            Assert.True(result.Count == 1);
            Assert.True(result.Contains("Mimir"));
        }

        [Theory]
        [InlineData("Mimir, ")]
        public void ConvertToArray_Returns_Correct_Array_When_One_Value_And_Comma(string value)
        {
            var result = value.ConvertToArray();
            Assert.True(result.Count == 1);
            Assert.True(result.Contains("Mimir"));
        }

        [Theory]
        [InlineData(" ", "")]
        public void ConvertToArray_Returns_Correct_Array_When_No_Values(string value1, string value2)
        {
            var result1 = value1.ConvertToArray();
            var result2 = value2.ConvertToArray();
            Assert.True(result1 is {Count: 0});
            Assert.True(result2 is {Count: 0});
        }

        [Theory]
        [InlineData("Mimir, mimir, tyle, tylE")]
        public void HasDuplicateValues_Returns_Correct_Boolean_If_Duplicates(string value)
        {
            var result = value.ConvertToArray();
            Assert.True(result.HasDuplicateValues());
        }

        [Theory]
        [InlineData("Mimir, Runir, tyle")]
        public void HasDuplicateValues_Returns_Correct_Boolean_If_No_Duplicates(string value)
        {
            var result = value.ConvertToArray();
            Assert.False(result.HasDuplicateValues());
        }

        [Theory]
        [InlineData("1.0")]
        public void IncrementMajorVersion_Two_Digits_Returns_Correct_Version(string value)
        {
            var result = value.IncrementMajorVersion();
            Assert.True(result == "2.0");
        }

        [Theory]
        [InlineData("1.0.0")]
        public void IncrementMajorVersion_Three_Digits_Returns_Correct_Version(string value)
        {
            var result = value.IncrementMajorVersion();
            Assert.True(result == "2.0.0");
        }

        [Theory]
        [InlineData("1.0")]
        public void IncrementMinorVersion_Two_Digits_Returns_Correct_Version(string value)
        {
            var result = value.IncrementMinorVersion();
            Assert.True(result == "1.1");
        }

        [Theory]
        [InlineData("1.0.0")]
        public void IncrementMinorVersion_ThreeDigits_Returns_Correct_Version(string value)
        {
            var result = value.IncrementMinorVersion();
            Assert.True(result == "1.1.0");
        }

        [Theory]
        [InlineData("1.0")]
        public void IncrementPatchVersion_Two_Digits_Returns_Correct_Version(string value)
        {
            var result = value.IncrementPatchVersion();
            Assert.True(result == "1.0");
        }
        
        [Theory]
        [InlineData("1.0.0")]
        public void IncrementPatchVersion_Three_Digits_Returns_Correct_Version(string value)
        {
            var result = value.IncrementPatchVersion();
            Assert.True(result == "1.0.1");
        }

        [Theory]
        [InlineData("99b212bf-013b-4b47-8ee7-192ba76ef5bd")]
        public void CreateSha512_Returns_Correct_Value(string value)
        {
            var result = value.CreateSha512();
            Assert.True(result == "96E479000CCE97488437A7E501EE171859373E273A1789133367D0B36520A6AA4592619A0E540291BDB70B9A27BC1B421D5F96B8BE2A92682F3C98B822C0E2B4");
        }

        [Theory]
        [InlineData("Mimir_99b212bf-013b, Mimir, Mimir-99b212bf-013b")]
        public void ResolveDomain_Returns_Correct_Value(string values)
        {
            var result = values.ConvertToArray();
            Assert.True(result.Count == 3);
            Assert.True(result.ElementAt(0).ResolveDomain() == "Mimir");
            Assert.True(result.ElementAt(1).ResolveDomain() == null);
            Assert.True(result.ElementAt(2).ResolveDomain() == null);
        }
    }
}