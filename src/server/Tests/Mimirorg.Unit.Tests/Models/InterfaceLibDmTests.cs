using System.Linq;
using Mimirorg.Setup;
using Mimirorg.Setup.Fixtures;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Extensions;
using Mimirorg.TypeLibrary.Models.Application;
using Xunit;

namespace Mimirorg.Unit.Tests.Models
{
    public class InterfaceLibDmTests : UnitTest<MimirorgCommonFixture>
    {
        private readonly MimirorgCommonFixture _fixture;

        public InterfaceLibDmTests(MimirorgCommonFixture fixture) : base(fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void HasIllegalChanges_Valid_Ok()
        {
            var dummy = _fixture.CreateInterfaceTestData();
            var status = dummy.dm.HasIllegalChanges(dummy.am);
            Assert.True(status.IsValid);
        }

        [Fact]
        public void HasIllegalChanges_Valid_False_When_Remove_Data_From_Lists()
        {
            var dummy = _fixture.CreateInterfaceTestData();

            // Reset changes
            dummy.am.AttributeIdList.Remove("123");

            var status = dummy.dm.HasIllegalChanges(dummy.am);
            Assert.False(status.IsValid);
            Assert.Single(status.Result);
        }

        [Fact]
        public void HasIllegalChanges_Valid_False_When_Not_Legal_Data_Is_Changed()
        {
            var dummy = _fixture.CreateInterfaceTestData();

            dummy.am.Name = "x";
            dummy.am.RdsName = "x";
            dummy.am.RdsCode = "x";
            dummy.am.Aspect = Aspect.NotSet;
            dummy.am.ParentId = "x";

            var status = dummy.dm.HasIllegalChanges(dummy.am);
            Assert.False(status.IsValid);
            Assert.Equal(5, status.Result.Count);
        }

        [Fact]
        public void CalculateVersionStatus_Validates_Correct_No_Change_Version()
        {
            var dummy = _fixture.CreateInterfaceTestData();

            // Reset changes
            dummy.am.AttributeIdList.Remove("555");

            var status = dummy.dm.CalculateVersionStatus(dummy.am);
            Assert.Equal(VersionStatus.NoChange, status);
        }

        [Fact]
        public void CalculateVersionStatus_Validates_Correct_Minor_Version()
        {
            var dummy = _fixture.CreateInterfaceTestData();

            // Reset changes
            dummy.am.AttributeIdList.Remove("555");

            // Trigger minor
            dummy.am.PurposeName = "x";
            dummy.am.CompanyId = 10;
            dummy.am.Description = "x";
            dummy.am.TypeReferences = dummy.am.TypeReferences.Where(x => x.Name != "XX").ToList();
            dummy.am.TypeReferences.Add(new TypeReferenceAm
            {
                Iri = "http://xxx.com",
                Name = "AA"
            });

            var status = dummy.dm.CalculateVersionStatus(dummy.am);
            Assert.Equal(VersionStatus.Minor, status);
        }

        [Fact]
        public void CalculateVersionStatus_Validates_Correct_Major_Version()
        {
            var dummy = _fixture.CreateInterfaceTestData();

            // Trigger minor
            dummy.am.PurposeName = "x";
            dummy.am.CompanyId = 10;
            dummy.am.Description = "x";
            dummy.am.TypeReferences = dummy.am.TypeReferences.Where(x => x.Name != "XX").ToList();
            dummy.am.TypeReferences.Add(new TypeReferenceAm
            {
                Iri = "http://xxx.com",
                Name = "AA"
            });

            var status = dummy.dm.CalculateVersionStatus(dummy.am);
            Assert.Equal(VersionStatus.Major, status);
        }
    }
}