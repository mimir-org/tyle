using AutoMapper;
using Tyle.Application.Attributes.Requests;
using Tyle.Persistence.Attributes;
using Tyle.Test.Setup;
using Tyle.Test.Setup.Fixtures;
using Xunit;

namespace Tyle.Test.Unit.Data.Attributes;

public class UnitRepositoryTests : UnitTest<MimirorgCommonFixture>
{
    private readonly UnitRepository _unitRepository;

    public UnitRepositoryTests(MimirorgCommonFixture fixture) : base(fixture)
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile(new UnitProfile()));
        _unitRepository = new UnitRepository(fixture.TyleContext, config.CreateMapper());
    }

    [Fact]
    public async Task CanCreateNewUnit()
    {
        var unit = new RdlUnitRequest
        {
            Name = "Test unit",
            Symbol = "tu",
            Description = "Test description",
            Iri = "http://test.com/unit"
        };

        var unitCollectionCount = (await _unitRepository.GetAll()).Count();

        var createdUnit = await _unitRepository.Create(unit);

        Assert.NotNull(createdUnit);
        Assert.Equal(unitCollectionCount + 1, (await _unitRepository.GetAll()).Count());
    }

    [Fact]
    public async Task GetUnitReturnsTheCreatedUnit()
    {
        var unit = new RdlUnitRequest
        {
            Name = "Test unit",
            Symbol = "tu",
            Description = "Test description",
            Iri = "http://test.com/unit"
        };

        var createdUnit = await _unitRepository.Create(unit);
        Assert.NotNull(createdUnit);

        Assert.Equal(unit.Name, createdUnit.Name);
        Assert.Equal(unit.Symbol, createdUnit.Symbol);
        Assert.Equal(unit.Description, createdUnit.Description);
        Assert.Equal(unit.Iri, createdUnit.Iri.OriginalString);
    }

    [Fact]
    public async Task GetUnitReturnsNullForInvalidId()
    {
        var ids = (await _unitRepository.GetAll()).Select(x => x.Id).ToList();

        var invalidId = 1;

        if (ids.Any())
        {
            invalidId = ids.Max() + 1;
        }

        Assert.Null(await _unitRepository.Get(invalidId));
    }

    [Fact]
    public async Task CanDeleteUnit()
    {
        var unit = new RdlUnitRequest
        {
            Name = "Test unit",
            Iri = "http://test.com/unit"
        };

        var createdUnit = await _unitRepository.Create(unit);
        var unitCount = (await _unitRepository.GetAll()).Count();

        Assert.True(await _unitRepository.Delete(createdUnit.Id));
        Assert.Equal(unitCount - 1, (await _unitRepository.GetAll()).Count());
    }

    [Fact]
    public async Task DeleteUnitReturnsFalseForInvalidId()
    {
        var ids = (await _unitRepository.GetAll()).Select(x => x.Id).ToList();

        var invalidId = 1;

        if (ids.Any())
        {
            invalidId = ids.Max() + 1;
        }

        Assert.False(await _unitRepository.Delete(invalidId));
    }
}