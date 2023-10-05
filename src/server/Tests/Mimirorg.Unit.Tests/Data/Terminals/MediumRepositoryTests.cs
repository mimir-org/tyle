using AutoMapper;
using Mimirorg.Test.Setup;
using Mimirorg.Test.Setup.Fixtures;
using TypeLibrary.Data.Terminals;
using TypeLibrary.Services.Terminals.Requests;
using Xunit;

namespace Mimirorg.Test.Unit.Data.Terminals;

public class MediumRepositoryTests : UnitTest<MimirorgCommonFixture>
{
    private readonly MediumRepository _mediumRepository;

    public MediumRepositoryTests(MimirorgCommonFixture fixture) : base(fixture)
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile(new MediumProfile()));
        _mediumRepository = new MediumRepository(fixture.TyleContext, config.CreateMapper());
    }

    [Fact]
    public async Task CanCreateNewMedium()
    {
        var medium = new RdlMediumRequest
        {
            Name = "Test medium",
            Description = "Test description",
            Iri = "http://test.com/medium"
        };

        var mediumCollectionCount = (await _mediumRepository.GetAll()).Count();

        var createdMedium = await _mediumRepository.Create(medium);

        Assert.NotNull(createdMedium);
        Assert.Equal(mediumCollectionCount + 1, (await _mediumRepository.GetAll()).Count());
    }

    [Fact]
    public async Task GetMediumReturnsTheCreatedUnit()
    {
        var medium = new RdlMediumRequest
        {
            Name = "Test medium",
            Description = "Test description",
            Iri = "http://test.com/medium"
        };

        var createdMedium = await _mediumRepository.Create(medium);
        Assert.NotNull(createdMedium);

        Assert.Equal(medium.Name, createdMedium.Name);
        Assert.Equal(medium.Description, createdMedium.Description);
        Assert.Equal(medium.Iri, createdMedium.Iri.OriginalString);
    }

    [Fact]
    public async Task GetMediumReturnsNullForInvalidId()
    {
        var ids = (await _mediumRepository.GetAll()).Select(x => x.Id).ToList();

        var invalidId = 1;

        if (ids.Any())
        {
            invalidId = ids.Max() + 1;
        }

        Assert.Null(await _mediumRepository.Get(invalidId));
    }

    [Fact]
    public async Task CanDeleteMedium()
    {
        var medium = new RdlMediumRequest
        {
            Name = "Test medium",
            Iri = "http://test.com/medium"
        };

        var createdMedium = await _mediumRepository.Create(medium);
        var mediumCount = (await _mediumRepository.GetAll()).Count();

        Assert.True(await _mediumRepository.Delete(createdMedium.Id));
        Assert.Equal(mediumCount - 1, (await _mediumRepository.GetAll()).Count());
    }

    [Fact]
    public async Task DeleteMediumReturnsFalseForInvalidId()
    {
        var ids = (await _mediumRepository.GetAll()).Select(x => x.Id).ToList();

        var invalidId = 1;

        if (ids.Any())
        {
            invalidId = ids.Max() + 1;
        }

        Assert.False(await _mediumRepository.Delete(invalidId));
    }
}