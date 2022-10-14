using System.Collections.Generic;
using System.Linq;
using Mimirorg.Common.Contracts;
using Mimirorg.Common.Enums;
using Mimirorg.Common.Extensions;
using Mimirorg.Setup;
using Mimirorg.Setup.Fixtures;
using Xunit;

namespace Mimirorg.Unit.Tests.Extensions
{
    public class EnumerableExtensionTests : UnitTest<MimirorgCommonFixture>
    {
        public EnumerableExtensionTests(MimirorgCommonFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public void LatestVersion_Returns_Correct()
        {
            var list = new List<IVersionObject>
            {
                new DummyVersionObject
                {
                    FirstVersionId = "123",
                    Version = "1.0"
                },
                new DummyVersionObject
                {
                    FirstVersionId = "123",
                    Version = "1.1"
                },
                new DummyVersionObject
                {
                    FirstVersionId = "567",
                    Version = "1.0"
                },
                new DummyVersionObject
                {
                    FirstVersionId = "123",
                    Version = "3.0"
                },
                new DummyVersionObject
                {
                    State = State.Deleted,
                    FirstVersionId = "123",
                    Version = "5.0"
                },
                new DummyVersionObject
                {
                    FirstVersionId = "123",
                    Version = "2.1"
                },
                new DummyVersionObject
                {
                    FirstVersionId = "123",
                    Version = "2.2"
                }
            };

            var latest = list.LatestVersionsExcludeDeleted().ToList();
            Assert.Equal(2, latest.Count);
            Assert.NotNull(latest.FirstOrDefault(x => x.FirstVersionId == "123" && x.Version == "3.0"));
            Assert.NotNull(latest.FirstOrDefault(x => x.FirstVersionId == "567" && x.Version == "1.0"));
        }

        protected internal class DummyVersionObject : IVersionObject
        {
            public State State { get; set; }
            public string FirstVersionId { get; set; }
            public string Version { get; set; }
        }

    }
}