using System.Linq;
using Mimirorg.Test.Setup;
using Mimirorg.Test.Setup.Fixtures;
using Mimirorg.TypeLibrary.Enums;
using Xunit;

namespace Mimirorg.Test.Unit.Models;

public class NodeLibDmTests : UnitTest<MimirorgCommonFixture>
{
    private readonly MimirorgCommonFixture _fixture;

    public NodeLibDmTests(MimirorgCommonFixture fixture) : base(fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void HasIllegalChanges_Valid_Ok()
    {
        var dummy = _fixture.CreateNodeTestData();
        var status = dummy.dm.HasIllegalChanges(dummy.am);
        Assert.True(status.IsValid);
    }

    [Fact]
    public void HasIllegalChanges_Valid_False_When_Remove_Data_From_Lists()
    {
        var dummy = _fixture.CreateNodeTestData();

        // Reset changes
        dummy.am.NodeTerminals = dummy.am.NodeTerminals.Where(x => x.TerminalId != 123).ToList();
        dummy.am.SelectedAttributePredefined = dummy.am.SelectedAttributePredefined.Where(x => x.Key != "123").ToList();

        var status = dummy.dm.HasIllegalChanges(dummy.am);
        Assert.False(status.IsValid);
        Assert.Equal(2, status.Result.Count);
    }

    [Fact]
    public void HasIllegalChanges_Valid_False_When_Not_Legal_Data_Is_Changed()
    {
        var dummy = _fixture.CreateNodeTestData();

        dummy.am.Name = "x";
        dummy.am.RdsName = "x";
        dummy.am.RdsCode = "x";
        dummy.am.Aspect = Aspect.NotSet;
        dummy.am.ParentId = 1;

        var status = dummy.dm.HasIllegalChanges(dummy.am);
        Assert.False(status.IsValid);
        Assert.Equal(5, status.Result.Count);
    }

    [Fact]
    public void CalculateVersionStatus_Validates_Correct_No_Change_Version()
    {
        var dummy = _fixture.CreateNodeTestData();

        // Reset changes
        //dummy.am.AttributeIdList.Remove("555");
        dummy.am.NodeTerminals = dummy.am.NodeTerminals.Where(x => x.TerminalId != 555).ToList();
        dummy.am.SelectedAttributePredefined = dummy.am.SelectedAttributePredefined.Where(x => x.Key != "555").ToList();

        var status = dummy.dm.CalculateVersionStatus(dummy.am);
        Assert.Equal(VersionStatus.NoChange, status);
    }

    [Fact]
    public void CalculateVersionStatus_Validates_Correct_Minor_Version()
    {
        var dummy = _fixture.CreateNodeTestData();

        // Reset changes
        //dummy.am.AttributeIdList.Remove("555");
        dummy.am.NodeTerminals = dummy.am.NodeTerminals.Where(x => x.TerminalId != 555).ToList();
        dummy.am.SelectedAttributePredefined = dummy.am.SelectedAttributePredefined.Where(x => x.Key != "555").ToList();

        // Trigger minor
        dummy.am.PurposeName = "x";
        dummy.am.CompanyId = 10;
        dummy.am.Description = "x";
        dummy.am.Symbol = "x";
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
        var dummy = _fixture.CreateNodeTestData();

        // Trigger minor
        dummy.am.PurposeName = "x";
        dummy.am.CompanyId = 10;
        dummy.am.Description = "x";
        dummy.am.Symbol = "x";
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