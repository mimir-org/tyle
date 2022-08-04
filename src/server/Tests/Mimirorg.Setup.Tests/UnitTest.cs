using Xunit;

namespace Mimirorg.Setup.Tests
{
    [Trait("Category", "Unit")]
    public abstract class UnitTest<TFixture> : IClassFixture<TFixture> where TFixture : class, new()
    {
        protected UnitTest(TFixture fixture)
        {

        }
    }
}