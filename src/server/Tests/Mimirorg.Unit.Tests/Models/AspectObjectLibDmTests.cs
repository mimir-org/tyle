using System;
using System.Collections.Generic;
using System.Linq;
using Mimirorg.Test.Setup;
using Mimirorg.Test.Setup.Fixtures;
using Mimirorg.TypeLibrary.Enums;
using Xunit;

namespace Mimirorg.Test.Unit.Models;

public class AspectObjectLibDmTests : UnitTest<MimirorgCommonFixture>
{
    private readonly MimirorgCommonFixture _fixture;

    public AspectObjectLibDmTests(MimirorgCommonFixture fixture) : base(fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void HasIllegalChanges_Valid_Ok()
    {
        var dummy = _fixture.CreateAspectObjectTestData();
        var status = dummy.dm.HasIllegalChanges(dummy.am);
        Assert.True(status.IsValid);
    }

    [Fact]
    public void HasIllegalChanges_Valid_False_When_Remove_Data_From_Lists()
    {
        var dummy = _fixture.CreateAspectObjectTestData();

        // Reset changes
        dummy.am.AspectObjectTerminals = dummy.am.AspectObjectTerminals.Where(x => x.TerminalId != "123").ToList();
        dummy.am.SelectedAttributePredefined = dummy.am.SelectedAttributePredefined.Where(x => x.Key != "123").ToList();

        var status = dummy.dm.HasIllegalChanges(dummy.am);
        Assert.False(status.IsValid);
        Assert.Equal(2, status.Result.Count);
    }

    [Fact]
    public void HasIllegalChanges_Valid_False_When_Not_Legal_Data_Is_Changed()
    {
        var dummy = _fixture.CreateAspectObjectTestData();

        dummy.am.Name = "x";
        dummy.am.RdsId = "x";
        dummy.am.Aspect = Aspect.NotSet;

        var status = dummy.dm.HasIllegalChanges(dummy.am);
        Assert.False(status.IsValid);
        Assert.Equal(3, status.Result.Count);
    }

    [Fact]
    public void CalculateVersionStatus_Validates_Correct_No_Change_Version()
    {
        var dummy = _fixture.CreateAspectObjectTestData();

        // Reset changes
        //dummy.am.AttributeIdList.Remove("555");
        dummy.am.AspectObjectTerminals = dummy.am.AspectObjectTerminals.Where(x => x.TerminalId != "555").ToList();
        dummy.am.SelectedAttributePredefined = dummy.am.SelectedAttributePredefined.Where(x => x.Key != "555").ToList();

        var status = dummy.dm.CalculateVersionStatus(dummy.am);
        Assert.Equal(VersionStatus.NoChange, status);
    }

    [Fact]
    public void CalculateVersionStatus_Validates_Correct_Minor_Version()
    {
        var dummy = _fixture.CreateAspectObjectTestData();

        // Reset changes
        //dummy.am.AttributeIdList.Remove("555");
        dummy.am.AspectObjectTerminals = dummy.am.AspectObjectTerminals.Where(x => x.TerminalId != "555").ToList();
        dummy.am.SelectedAttributePredefined = dummy.am.SelectedAttributePredefined.Where(x => x.Key != "555").ToList();

        // Trigger minor
        dummy.am.PurposeName = "x";
        dummy.am.CompanyId = 10;
        dummy.am.Description = "x";
        dummy.am.Symbol = "x";
        dummy.am.TypeReference = "x";

        var status = dummy.dm.CalculateVersionStatus(dummy.am);
        Assert.Equal(VersionStatus.Minor, status);
    }

    [Fact]
    public void CalculateVersionStatus_Validates_Correct_Major_Version()
    {
        var dummy = _fixture.CreateAspectObjectTestData();

        // Trigger minor
        dummy.am.PurposeName = "x";
        dummy.am.CompanyId = 10;
        dummy.am.Description = "x";
        dummy.am.Symbol = "x";
        dummy.am.TypeReference = "x";

        var status = dummy.dm.CalculateVersionStatus(dummy.am);
        Assert.Equal(VersionStatus.Major, status);
    }

    [Fact]
    public void CalculateVersionStatus_Validates_Correct_Major_Version_On_Attribute_Add()
    {
        var dummy = _fixture.CreateAspectObjectTestData();

        // Reset changes
        //dummy.am.AttributeIdList.Remove("555");
        dummy.am.AspectObjectTerminals = dummy.am.AspectObjectTerminals.Where(x => x.TerminalId != "555").ToList();
        dummy.am.SelectedAttributePredefined = dummy.am.SelectedAttributePredefined.Where(x => x.Key != "555").ToList();

        // Trigger minor
        dummy.am.PurposeName = "x";
        dummy.am.CompanyId = 10;
        dummy.am.Description = "x";
        dummy.am.Symbol = "x";
        dummy.am.TypeReference = "x";

        // Trigger major
        dummy.am.Attributes = new List<string>
        {
            "123456"
        };

        var status = dummy.dm.CalculateVersionStatus(dummy.am);
        Assert.Equal(VersionStatus.Major, status);
    }

    [Fact]
    public void CalculateVersionStatus_Throws_Exception_On_Null()
    {
        var dummy = _fixture.CreateAspectObjectTestData();

        Assert.Throws<ArgumentNullException>(() => dummy.dm.CalculateVersionStatus(null));
    }

    [Fact]
    public void CalculateVersionStatus_Handles_Null_Terminals_SelectedAttributes()
    {
        var dummy = _fixture.CreateAspectObjectTestData();

        dummy.am.AspectObjectTerminals = null;
        dummy.am.SelectedAttributePredefined = null;

        dummy.dm.AspectObjectTerminals = null;
        dummy.dm.SelectedAttributePredefined = null;

        var status = dummy.dm.CalculateVersionStatus(dummy.am);
        Assert.Equal(VersionStatus.NoChange, status);
    }
}