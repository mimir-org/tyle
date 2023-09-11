using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Mimirorg.Common.Exceptions;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Services;

public class ReferenceService : IReferenceService
{
    private readonly IMapper _mapper;
    private readonly IEfClassifierRepository _classifierRepository;
    private readonly IEfMediumRepository _mediumRepository;
    private readonly IEfPredicateRepository _predicateRepository;
    private readonly IEfPurposeRepository _purposeRepository;
    private readonly IEfUnitRepository _unitRepository;

    public ReferenceService(IMapper mapper, IEfClassifierRepository classifierRepository, IEfMediumRepository mediumRepository, IEfPredicateRepository predicateRepository, IEfPurposeRepository purposeRepository, IEfUnitRepository unitRepository)
    {
        _mapper = mapper;
        _classifierRepository = classifierRepository;
        _mediumRepository = mediumRepository;
        _predicateRepository = predicateRepository;
        _purposeRepository = purposeRepository;
        _unitRepository = unitRepository;
    }

    public IEnumerable<ClassifierReferenceCm> GetAllClassifiers()
    {
        var classifiers = _classifierRepository.GetAll().ToList();

        return !classifiers.Any() ? new List<ClassifierReferenceCm>() : _mapper.Map<List<ClassifierReferenceCm>>(classifiers);
    }

    public IEnumerable<MediumReferenceCm> GetAllMedia()
    {
        var media = _mediumRepository.GetAll().ToList();

        return !media.Any() ? new List<MediumReferenceCm>() : _mapper.Map<List<MediumReferenceCm>>(media);
    }

    public IEnumerable<PredicateReferenceCm> GetAllPredicates()
    {
        var predicates = _predicateRepository.GetAll().ToList();

        return !predicates.Any() ? new List<PredicateReferenceCm>() : _mapper.Map<List<PredicateReferenceCm>>(predicates);
    }

    public IEnumerable<PurposeReferenceCm> GetAllPurposes()
    {
        var purposes = _purposeRepository.GetAll().ToList();

        return !purposes.Any() ? new List<PurposeReferenceCm>() : _mapper.Map<List<PurposeReferenceCm>>(purposes);
    }

    public IEnumerable<UnitReferenceCm> GetAllUnits()
    {
        var units = _unitRepository.GetAll().ToList();

        return !units.Any() ? new List<UnitReferenceCm>() : _mapper.Map<List<UnitReferenceCm>>(units);
    }

    public async Task<ClassifierReferenceCm> GetClassifier(int id)
    {
        var classifier = await _classifierRepository.GetAsync(id) ?? throw new MimirorgNotFoundException($"Classifier with id {id} not found.");
        return _mapper.Map<ClassifierReferenceCm>(classifier);
    }

    public async Task<MediumReferenceCm> GetMedium(int id)
    {
        var medium = await _mediumRepository.GetAsync(id) ?? throw new MimirorgNotFoundException($"Medium with id {id} not found.");
        return _mapper.Map<MediumReferenceCm>(medium);
    }

    public async Task<PredicateReferenceCm> GetPredicate(int id)
    {
        var predicate = await _predicateRepository.GetAsync(id) ?? throw new MimirorgNotFoundException($"Predicate with id {id} not found.");
        return _mapper.Map<PredicateReferenceCm>(predicate);
    }

    public async Task<PurposeReferenceCm> GetPurpose(int id)
    {
        var purpose = await _purposeRepository.GetAsync(id) ?? throw new MimirorgNotFoundException($"Purpose with id {id} not found.");
        return _mapper.Map<PurposeReferenceCm>(purpose);
    }

    public async Task<UnitReferenceCm> GetUnit(int id)
    {
        var unit = await _unitRepository.GetAsync(id) ?? throw new MimirorgNotFoundException($"Unit with id {id} not found.");
        return _mapper.Map<UnitReferenceCm>(unit);
    }

    public async Task<ClassifierReferenceCm> CreateClassifier(ClassifierReferenceAm classifierAm)
    {
        if (classifierAm == null) throw new ArgumentNullException(nameof(classifierAm));

        var classifier = _mapper.Map<ClassifierReference>(classifierAm);
        await _classifierRepository.CreateAsync(classifier);
        await _classifierRepository.SaveAsync();
        _classifierRepository.Detach(classifier);

        return _mapper.Map<ClassifierReferenceCm>(classifier);
    }

    public async Task<MediumReferenceCm> CreateMedium(MediumReferenceAm mediumAm)
    {
        if (mediumAm == null) throw new ArgumentNullException(nameof(mediumAm));

        var medium = _mapper.Map<MediumReference>(mediumAm);
        await _mediumRepository.CreateAsync(medium);
        await _mediumRepository.SaveAsync();
        _mediumRepository.Detach(medium);

        return _mapper.Map<MediumReferenceCm>(medium);
    }

    public async Task<PredicateReferenceCm> CreatePredicate(PredicateReferenceRequest predicateAm)
    {
        if (predicateAm == null) throw new ArgumentNullException(nameof(predicateAm));

        var predicate = _mapper.Map<PredicateReference>(predicateAm);
        await _predicateRepository.CreateAsync(predicate);
        await _predicateRepository.SaveAsync();
        _predicateRepository.Detach(predicate);

        return _mapper.Map<PredicateReferenceCm>(predicate);
    }

    public async Task<PurposeReferenceCm> CreatePurpose(PurposeReferenceAm purposeAm)
    {
        if (purposeAm == null) throw new ArgumentNullException(nameof(purposeAm));

        var purpose = _mapper.Map<PurposeReference>(purposeAm);
        await _purposeRepository.CreateAsync(purpose);
        await _purposeRepository.SaveAsync();
        _purposeRepository.Detach(purpose);

        return _mapper.Map<PurposeReferenceCm>(purpose);
    }

    public async Task<UnitReferenceCm> CreateUnit(UnitReferenceRequest unitRequest)
    {
        if (unitRequest == null) throw new ArgumentNullException(nameof(unitRequest));

        var unit = _mapper.Map<UnitReference>(unitRequest);
        await _unitRepository.CreateAsync(unit);
        await _unitRepository.SaveAsync();
        _unitRepository.Detach(unit);

        return _mapper.Map<UnitReferenceCm>(unit);
    }

    public Task DeleteClassifier(int id)
    {
        throw new System.NotImplementedException();
    }

    public Task DeleteMedium(int id)
    {
        throw new System.NotImplementedException();
    }

    public Task DeletePredicate(int id)
    {
        throw new System.NotImplementedException();
    }

    public Task DeletePurpose(int id)
    {
        throw new System.NotImplementedException();
    }

    public Task DeleteUnit(int id)
    {
        throw new System.NotImplementedException();
    }
}