using AutoMapper;
using Tyle.Application.Common.Requests;
using Tyle.Persistence.Common;
using Tyle.Test.Setup;
using Tyle.Test.Setup.Fixtures;
using Xunit;

namespace Tyle.Test.Unit.Data.Common;

public class ClassifierRepositoryTests : UnitTest<MimirorgCommonFixture>
{
    private readonly ClassifierRepository _classifierRepository;

    public ClassifierRepositoryTests(MimirorgCommonFixture fixture) : base(fixture)
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile(new ClassifierProfile()));
        _classifierRepository = new ClassifierRepository(fixture.TyleContext, config.CreateMapper());
    }

    [Fact]
    public async Task CanCreateNewClassifier()
    {
        var classifier = new RdlClassifierRequest
        {
            Name = "Test classifier",
            Description = "Test description",
            Iri = "http://test.com/classifier"
        };

        var classifierCollectionCount = (await _classifierRepository.GetAll()).Count();

        var createdClassifier = await _classifierRepository.Create(classifier);

        Assert.NotNull(createdClassifier);
        Assert.Equal(classifierCollectionCount + 1, (await _classifierRepository.GetAll()).Count());
    }

    [Fact]
    public async Task GetClassifierReturnsTheCreatedUnit()
    {
        var classifier = new RdlClassifierRequest
        {
            Name = "Test classifier",
            Description = "Test description",
            Iri = "http://test.com/classifier"
        };

        var createdClassifier = await _classifierRepository.Create(classifier);
        Assert.NotNull(createdClassifier);

        Assert.Equal(classifier.Name, createdClassifier.Name);
        Assert.Equal(classifier.Description, createdClassifier.Description);
        Assert.Equal(classifier.Iri, createdClassifier.Iri.OriginalString);
    }

    [Fact]
    public async Task GetClassifierReturnsNullForInvalidId()
    {
        var ids = (await _classifierRepository.GetAll()).Select(x => x.Id).ToList();

        var invalidId = 1;

        if (ids.Any())
        {
            invalidId = ids.Max() + 1;
        }

        Assert.Null(await _classifierRepository.Get(invalidId));
    }

    [Fact]
    public async Task CanDeleteClassifier()
    {
        var classifier = new RdlClassifierRequest
        {
            Name = "Test classifier",
            Iri = "http://test.com/classifier"
        };

        var createdClassifier = await _classifierRepository.Create(classifier);
        var classifierCount = (await _classifierRepository.GetAll()).Count();

        Assert.True(await _classifierRepository.Delete(createdClassifier.Id));
        Assert.Equal(classifierCount - 1, (await _classifierRepository.GetAll()).Count());
    }

    [Fact]
    public async Task DeleteClassifierReturnsFalseForInvalidId()
    {
        var ids = (await _classifierRepository.GetAll()).Select(x => x.Id).ToList();

        var invalidId = 1;

        if (ids.Any())
        {
            invalidId = ids.Max() + 1;
        }

        Assert.False(await _classifierRepository.Delete(invalidId));
    }
}