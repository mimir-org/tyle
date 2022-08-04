using Mimirorg.Setup.Tests;
using Mimirorg.TypeLibrary.Extensions;
using Xunit;

namespace Mimirorg.Unit.Tests.Extensions
{
    public class DateTimeExtensionTests : IClassFixture<MimirorgCommonFixture>
    {
        [Theory]
        [InlineData("2022-01-01T00:00:00 +03:00")]
        [InlineData("2022-01-01T00:00:00z")]
        [InlineData("2022-01-01T00:00:00Z")]
        [InlineData("2022-01-01T00:00:00 z")]
        [InlineData("2022-01-01T00:00:00 Z")]
        public void ParseUtcDateTime_Returns_UTC_DateTime_Value(string dateTime)
        {
            var result = dateTime.ParseUtcDateTime();
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData("2022-01-01T00:00:00")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("2022-01-01")]
        public void ParseUtcDateTime_Returns_Null_UTC_DateTime_Value(string dateTime)
        {
            var result = dateTime.ParseUtcDateTime();
            Assert.Null(result);
        }
    }
}