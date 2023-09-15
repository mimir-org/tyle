using Tyle.Application.Attributes.Requests;
using Tyle.Application.Common.Requests;
using Tyle.Application.Terminals.Requests;
using Tyle.Core.Attributes;
using Tyle.Core.Common;
using Tyle.Core.Common.Exceptions;
using Tyle.Core.Terminals;

namespace Tyle.Application.Common;

public class ReferenceService : IReferenceService
{
    private readonly IClassifierRepository _classifierRepository;
    private readonly IMediumRepository _mediumRepository;
    private readonly IPredicateRepository _predicateRepository;
    private readonly IPurposeRepository _purposeRepository;
    private readonly IUnitRepository _unitRepository;

    public ReferenceService(IClassifierRepository classifierRepository, IMediumRepository mediumRepository, IPredicateRepository predicateRepository, IPurposeRepository purposeRepository, IUnitRepository unitRepository)
    {
        _classifierRepository = classifierRepository;
        _mediumRepository = mediumRepository;
        _predicateRepository = predicateRepository;
        _purposeRepository = purposeRepository;
        _unitRepository = unitRepository;
    }

    public async Task<IEnumerable<ClassifierReference>> GetAllClassifiers()
    {
        return await _classifierRepository.GetAll();
    }

    public async Task<IEnumerable<MediumReference>> GetAllMedia()
    {
        return await _mediumRepository.GetAll();
    }

    public async Task<IEnumerable<PredicateReference>> GetAllPredicates()
    {
        return await _predicateRepository.GetAll();
    }

    public async Task<IEnumerable<PurposeReference>> GetAllPurposes()
    {
        return await _purposeRepository.GetAll();
    }

    public async Task<IEnumerable<UnitReference>> GetAllUnits()
    {
        return await _unitRepository.GetAll();
    }

    public async Task<ClassifierReference> GetClassifier(int id)
    {
        var classifier = await _classifierRepository.Get(id) ?? throw new MimirorgNotFoundException($"Classifier with id {id} not found.");
        return classifier;
    }

    public async Task<MediumReference> GetMedium(int id)
    {
        var medium = await _mediumRepository.Get(id) ?? throw new MimirorgNotFoundException($"Medium with id {id} not found.");
        return medium;
    }

    public async Task<PredicateReference> GetPredicate(int id)
    {
        var predicate = await _predicateRepository.Get(id) ?? throw new MimirorgNotFoundException($"Predicate with id {id} not found.");
        return predicate;
    }

    public async Task<PurposeReference> GetPurpose(int id)
    {
        var purpose = await _purposeRepository.Get(id) ?? throw new MimirorgNotFoundException($"Purpose with id {id} not found.");
        return purpose;
    }

    public async Task<UnitReference> GetUnit(int id)
    {
        var unit = await _unitRepository.Get(id) ?? throw new MimirorgNotFoundException($"Unit with id {id} not found.");
        return unit;
    }

    public async Task<ClassifierReference> CreateClassifier(ClassifierReferenceRequest request)
    {
        var classifier = new ClassifierReference(request.Name, request.Iri, request.Description);
        await _classifierRepository.Create(classifier);

        return await GetClassifier(classifier.Id);
    }

    public async Task<MediumReference> CreateMedium(MediumReferenceRequest request)
    {
        var medium = new MediumReference(request.Name, request.Iri, request.Description);
        await _mediumRepository.Create(medium);

        return await GetMedium(medium.Id);
    }

    public async Task<PredicateReference> CreatePredicate(PredicateReferenceRequest request)
    {
        var predicate = new PredicateReference(request.Name, request.Iri, request.Description);
        await _predicateRepository.Create(predicate);

        return await GetPredicate(predicate.Id);
    }

    public async Task<PurposeReference> CreatePurpose(PurposeReferenceRequest request)
    {
        var purpose = new PurposeReference(request.Name, request.Iri, request.Description);
        await _purposeRepository.Create(purpose);

        return await GetPurpose(purpose.Id);
    }

    public async Task<UnitReference> CreateUnit(UnitReferenceRequest request)
    {
        var unit = new UnitReference(request.Name, request.Iri, request.Symbol, request.Description);
        await _unitRepository.Create(unit);

        return await GetUnit(unit.Id);
    }

    public async Task DeleteClassifier(int id)
    {
        await _classifierRepository.Delete(id);
    }

    public async Task DeleteMedium(int id)
    {
        await _mediumRepository.Delete(id);
    }

    public async Task DeletePredicate(int id)
    {
        await _predicateRepository.Delete(id);
    }

    public async Task DeletePurpose(int id)
    {
        await _purposeRepository.Delete(id);
    }

    public async Task DeleteUnit(int id)
    {
        await _unitRepository.Delete(id);
    }
}