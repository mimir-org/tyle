using System.Collections.Generic;
using System.Linq;
using Mimirorg.Test.Setup;
using Mimirorg.Test.Setup.Fixtures;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Extensions;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using Xunit;

namespace Mimirorg.Test.Unit.Models
{
    public class TransportLibDmTests : UnitTest<MimirorgCommonFixture>
    {
        private readonly MimirorgCommonFixture _fixture;

        public TransportLibDmTests(MimirorgCommonFixture fixture) : base(fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void HasIllegalChanges_Valid_Ok()
        {
            var dummy = _fixture.CreateTransportTestData();
            var status = dummy.dm.HasIllegalChanges(dummy.am);
            Assert.True(status.IsValid);
        }

        [Fact]
        public void HasIllegalChanges_Valid_False_When_Remove_Data_From_Lists()
        {
            var dummy = _fixture.CreateTransportTestData();
            dummy.am.Attributes = new List<TypeReferenceAm>();

            var status = dummy.dm.HasIllegalChanges(dummy.am);
            Assert.False(status.IsValid);
            Assert.True(status.Result.Count == 2);
        }

        [Fact]
        public void HasIllegalChanges_Valid_False_When_Not_Legal_Data_Is_Changed()
        {
            var dummy = _fixture.CreateTransportTestData();

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
            var dummy = _fixture.CreateTransportTestData();

            // Reset changes
            //dummy.am.AttributeIdList.Remove("555");

            var status = dummy.dm.CalculateVersionStatus(dummy.am);
            Assert.Equal(VersionStatus.NoChange, status);
        }

        [Fact]
        public void CalculateVersionStatus_Validates_Correct_Minor_Version()
        {
            var dummy = _fixture.CreateTransportTestData();

            // Reset changes
            //dummy.am.AttributeIdList.Remove("555");

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
            var dummy = _fixture.CreateTransportTestData();

            var newAttribute = new TypeReferenceAm
            {
                Name = "a11",
                Iri = "http://rds.posccaesar.org/ontology/plm/rdl/PCA_a11",
                Source = "PCA",
                Units = new List<TypeReferenceSub>
                {
                    new()
                    {
                        Name = "u11",
                        Iri = "http://rds.posccaesar.org/ontology/plm/rdl/PCA_u11",
                        IsDefault = true
                    },
                    new()
                    {
                        Name = "u22",
                        Iri = "http://rds.posccaesar.org/ontology/plm/rdl/PCA_u22",
                        IsDefault = false
                    }
                }
            };

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
            dummy.am.Attributes.Add(newAttribute);

            var status = dummy.dm.CalculateVersionStatus(dummy.am);
            Assert.Equal(VersionStatus.Major, status);
        }
    }
}