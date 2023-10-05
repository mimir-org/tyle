using AutoMapper;
using Mimirorg.Test.Setup;
using Mimirorg.Test.Setup.Fixtures;
using TypeLibrary.Data.Attributes;
using TypeLibrary.Services.Attributes.Requests;
using Xunit;

namespace Mimirorg.Test.Unit.Data.Attributes;

public class PredicateRepositoryTests : UnitTest<MimirorgCommonFixture>
{
    private readonly PredicateRepository _predicateRepository;

    public PredicateRepositoryTests(MimirorgCommonFixture fixture) : base(fixture)
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile(new PredicateProfile()));
        _predicateRepository = new PredicateRepository(fixture.TyleContext, config.CreateMapper());
    }

    [Fact]
    public async Task CanCreateNewPredicate()
    {
        var predicate = new RdlPredicateRequest
        {
            Name = "Test predicate",
            Description = "Test description",
            Iri = "http://test.com/predicate"
        };

        var predicateCollectionCount = (await _predicateRepository.GetAll()).Count();

        var createdPredicate = await _predicateRepository.Create(predicate);

        Assert.NotNull(createdPredicate);
        Assert.Equal(predicateCollectionCount + 1, (await _predicateRepository.GetAll()).Count());
    }

    [Fact]
    public async Task GetPredicateReturnsTheCreatedUnit()
    {
        var predicate = new RdlPredicateRequest
        {
            Name = "Test predicate",
            Description = "Test description",
            Iri = "http://test.com/predicate"
        };

        var createdPredicate = await _predicateRepository.Create(predicate);
        Assert.NotNull(createdPredicate);

        Assert.Equal(predicate.Name, createdPredicate.Name);
        Assert.Equal(predicate.Description, createdPredicate.Description);
        Assert.Equal(predicate.Iri, createdPredicate.Iri.OriginalString);
    }

    [Fact]
    public async Task GetPredicateReturnsNullForInvalidId()
    {
        var ids = (await _predicateRepository.GetAll()).Select(x => x.Id).ToList();

        var invalidId = 1;

        if (ids.Any())
        {
            invalidId = ids.Max() + 1;
        }

        Assert.Null(await _predicateRepository.Get(invalidId));
    }

    [Fact]
    public async Task CanDeletePredicate()
    {
        var predicate = new RdlPredicateRequest
        {
            Name = "Test predicate",
            Iri = "http://test.com/predicate"
        };

        var createdPredicate = await _predicateRepository.Create(predicate);
        var predicateCount = (await _predicateRepository.GetAll()).Count();

        Assert.True(await _predicateRepository.Delete(createdPredicate.Id));
        Assert.Equal(predicateCount - 1, (await _predicateRepository.GetAll()).Count());
    }

    [Fact]
    public async Task DeletePredicateReturnsFalseForInvalidId()
    {
        var ids = (await _predicateRepository.GetAll()).Select(x => x.Id).ToList();

        var invalidId = 1;

        if (ids.Any())
        {
            invalidId = ids.Max() + 1;
        }

        Assert.False(await _predicateRepository.Delete(invalidId));
    }
}