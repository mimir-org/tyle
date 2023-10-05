using AutoMapper;
using Mimirorg.Test.Setup;
using Mimirorg.Test.Setup.Fixtures;
using TypeLibrary.Data.Common;
using TypeLibrary.Services.Common.Requests;
using Xunit;

namespace Mimirorg.Test.Unit.Data.Common;

public class PurposeRepositoryTests : UnitTest<MimirorgCommonFixture>
{
    private readonly PurposeRepository _purposeRepository;

    public PurposeRepositoryTests(MimirorgCommonFixture fixture) : base(fixture)
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile(new PurposeProfile()));
        _purposeRepository = new PurposeRepository(fixture.TyleContext, config.CreateMapper());
    }

    [Fact]
    public async Task CanCreateNewPurpose()
    {
        var purpose = new RdlPurposeRequest
        {
            Name = "Test purpose",
            Description = "Test description",
            Iri = "http://test.com/purpose"
        };

        var purposeCollectionCount = (await _purposeRepository.GetAll()).Count();

        var createdPurpose = await _purposeRepository.Create(purpose);

        Assert.NotNull(createdPurpose);
        Assert.Equal(purposeCollectionCount + 1, (await _purposeRepository.GetAll()).Count());
    }

    [Fact]
    public async Task GetPurposeReturnsTheCreatedUnit()
    {
        var purpose = new RdlPurposeRequest
        {
            Name = "Test purpose",
            Description = "Test description",
            Iri = "http://test.com/purpose"
        };

        var createdPurpose = await _purposeRepository.Create(purpose);
        Assert.NotNull(createdPurpose);

        Assert.Equal(purpose.Name, createdPurpose.Name);
        Assert.Equal(purpose.Description, createdPurpose.Description);
        Assert.Equal(purpose.Iri, createdPurpose.Iri.OriginalString);
    }

    [Fact]
    public async Task GetPurposeReturnsNullForInvalidId()
    {
        var ids = (await _purposeRepository.GetAll()).Select(x => x.Id).ToList();

        var invalidId = 1;

        if (ids.Any())
        {
            invalidId = ids.Max() + 1;
        }

        Assert.Null(await _purposeRepository.Get(invalidId));
    }

    [Fact]
    public async Task CanDeletePurpose()
    {
        var purpose = new RdlPurposeRequest
        {
            Name = "Test purpose",
            Iri = "http://test.com/purpose"
        };

        var createdPurpose = await _purposeRepository.Create(purpose);
        var purposeCount = (await _purposeRepository.GetAll()).Count();

        Assert.True(await _purposeRepository.Delete(createdPurpose.Id));
        Assert.Equal(purposeCount - 1, (await _purposeRepository.GetAll()).Count());
    }

    [Fact]
    public async Task DeletePurposeReturnsFalseForInvalidId()
    {
        var ids = (await _purposeRepository.GetAll()).Select(x => x.Id).ToList();

        var invalidId = 1;

        if (ids.Any())
        {
            invalidId = ids.Max() + 1;
        }

        Assert.False(await _purposeRepository.Delete(invalidId));
    }
}