using Tyle.Application.Attributes;
using Tyle.Application.Attributes.Requests;
using Tyle.Application.Common.Requests;
using Tyle.Application.Terminals;
using Tyle.Application.Terminals.Requests;
using Tyle.Core.Attributes;
using Tyle.Core.Common;
using Tyle.Core.Terminals;

namespace Tyle.Application.Common;

public class ReferenceService : IReferenceService
{
    private readonly IClassifierRepository _classifierRepository;
    //private readonly IMediumRepository _mediumRepository;
    //private readonly IPredicateRepository _predicateRepository;
    //private readonly IPurposeRepository _purposeRepository;
    //private readonly IUnitRepository _unitRepository;

    public ReferenceService(IClassifierRepository classifierRepository) //, IMediumRepository mediumRepository, IPredicateRepository predicateRepository, IPurposeRepository purposeRepository, IUnitRepository unitRepository)
    {
        _classifierRepository = classifierRepository;
        //_mediumRepository = mediumRepository;
        //_predicateRepository = predicateRepository;
        //_purposeRepository = purposeRepository;
        //_unitRepository = unitRepository;
    }

    public async Task<IEnumerable<ClassifierReference>> GetAllClassifiers()
    {
        return await _classifierRepository.GetAll();
    }

    /*public async Task<IEnumerable<MediumReference>> GetAllMedia()
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
    }*/

    public async Task<ClassifierReference?> GetClassifier(int id)
    {
        return await _classifierRepository.Get(id);
    }

    /*public async Task<MediumReference?> GetMedium(int id)
    {
        return await _mediumRepository.Get(id);
    }

    public async Task<PredicateReference?> GetPredicate(int id)
    {
        return await _predicateRepository.Get(id);
    }

    public async Task<PurposeReference?> GetPurpose(int id)
    {
        return await _purposeRepository.Get(id);
    }

    public async Task<UnitReference?> GetUnit(int id)
    {
        return await _unitRepository.Get(id);
    }*/

    public async Task<ClassifierReference> CreateClassifier(ClassifierReferenceRequest request)
    {
        var classifier = new ClassifierReference(request.Name, new Uri(request.Iri), request.Description);
        return await _classifierRepository.Create(classifier);
    }

    /*public async Task<MediumReference> CreateMedium(MediumReferenceRequest request)
    {
        var medium = new MediumReference(request.Name, new Uri(request.Iri), request.Description);
        return await _mediumRepository.Create(medium);
    }

    public async Task<PredicateReference> CreatePredicate(PredicateReferenceRequest request)
    {
        var predicate = new PredicateReference(request.Name, new Uri(request.Iri), request.Description);
        return await _predicateRepository.Create(predicate);
    }

    public async Task<PurposeReference> CreatePurpose(PurposeReferenceRequest request)
    {
        var purpose = new PurposeReference(request.Name, new Uri(request.Iri), request.Description);
        return await _purposeRepository.Create(purpose);
    }

    public async Task<UnitReference> CreateUnit(UnitReferenceRequest request)
    {
        var unit = new UnitReference(request.Name, new Uri(request.Iri), request.Symbol, request.Description);
        return await _unitRepository.Create(unit);
    }*/

    public async Task DeleteClassifier(int id)
    {
        await _classifierRepository.Delete(id);
    }

    /*public async Task DeleteMedium(int id)
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
    }*/
}