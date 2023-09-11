using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;

namespace TypeLibrary.Services.Contracts;

public interface IReferenceService
{
    IEnumerable<ClassifierReferenceCm> GetAllClassifiers();
    IEnumerable<MediumReferenceCm> GetAllMedia();
    IEnumerable<PredicateReferenceCm> GetAllPredicates();
    IEnumerable<PurposeReferenceCm> GetAllPurposes();
    IEnumerable<UnitReferenceCm> GetAllUnits();

    Task<ClassifierReferenceCm> GetClassifier(int id);
    Task<MediumReferenceCm> GetMedium(int id);
    Task<PredicateReferenceCm> GetPredicate(int id);
    Task<PurposeReferenceCm> GetPurpose(int id);
    Task<UnitReferenceCm> GetUnit(int id);

    Task<ClassifierReferenceCm> CreateClassifier(ClassifierReferenceAm classifierAm);
    Task<MediumReferenceCm> CreateMedium(MediumReferenceAm mediumAm);
    Task<PredicateReferenceCm> CreatePredicate(PredicateReferenceRequest predicateAm);
    Task<PurposeReferenceCm> CreatePurpose(PurposeReferenceAm purposeAm);
    Task<UnitReferenceCm> CreateUnit(UnitReferenceRequest unitRequest);

    Task DeleteClassifier(int id);
    Task DeleteMedium(int id);
    Task DeletePredicate(int id);
    Task DeletePurpose(int id);
    Task DeleteUnit(int id);
}