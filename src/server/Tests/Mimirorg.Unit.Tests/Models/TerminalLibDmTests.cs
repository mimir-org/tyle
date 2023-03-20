using System.Collections.Generic;
using Mimirorg.Test.Setup;
using Mimirorg.Test.Setup.Fixtures;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Xunit;

namespace Mimirorg.Test.Unit.Models;

public class TerminalLibDmTests : UnitTest<MimirorgCommonFixture>
{
    private readonly MimirorgCommonFixture _fixture;

    public TerminalLibDmTests(MimirorgCommonFixture fixture) : base(fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void HasIllegalChanges_Valid_Ok()
    {
        var dummy = _fixture.CreateTerminalTestData();
        var status = dummy.dm.HasIllegalChanges(dummy.am);
        Assert.True(status.IsValid);
    }

    [Fact]
    public void HasIllegalChanges_Valid_False_When_Remove_Data_From_Lists()
    {
        var dummy = _fixture.CreateTerminalTestData();
        dummy.am.Name = "NewName";
        var status = dummy.dm.HasIllegalChanges(dummy.am);
        Assert.False(status.IsValid);
        Assert.Single(status.Result);
    }

    [Fact]
    public void HasIllegalChanges_Valid_False_When_Not_Legal_Data_Is_Changed()
    {
        var dummy = _fixture.CreateTerminalTestData();

        dummy.am.Name = "x";
        dummy.am.ParentId = 1;

        var status = dummy.dm.HasIllegalChanges(dummy.am);
        Assert.False(status.IsValid);
        Assert.Equal(2, status.Result.Count);
    }

    [Fact]
    public void CalculateVersionStatus_Validates_Correct_No_Change_Version()
    {
        var dummy = _fixture.CreateTerminalTestData();
        var status = dummy.dm.CalculateVersionStatus(dummy.am);
        Assert.Equal(VersionStatus.NoChange, status);
    }

    [Fact]
    public void CalculateVersionStatus_Validates_Correct_Minor_Version()
    {
        var dummy = _fixture.CreateTerminalTestData();

        // Trigger minor
        dummy.am.Description = "x";

        var status = dummy.dm.CalculateVersionStatus(dummy.am);
        Assert.Equal(VersionStatus.Minor, status);
    }

    [Fact]
    public void CalculateVersionStatus_Validates_Correct_Major_Version()
    {
        var dummy = _fixture.CreateTerminalTestData();

        var newAttribute = new AttributeLibAm
        {
            Name = "a11",
            Iri = "http://rds.posccaesar.org/ontology/plm/rdl/PCA_a11",
            Source = "PCA",
            Units = new List<UnitLibAm>
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

        dummy.am.Description = "x";
        dummy.am.Attributes.Add(newAttribute);

        var status = dummy.dm.CalculateVersionStatus(dummy.am);
        Assert.Equal(VersionStatus.Major, status);
    }
}