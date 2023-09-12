using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using Mimirorg.TypeLibrary.Models.Domain;

namespace TypeLibrary.Services.Contracts;

public interface IReferenceService
{
    IEnumerable<ClassifierReferenceCm> GetAllClassifiers();
    IEnumerable<MediumReferenceCm> GetAllMedia();
    IEnumerable<PredicateReference> GetAllPredicates();
    IEnumerable<PurposeReferenceCm> GetAllPurposes();
    IEnumerable<UnitReference> GetAllUnits();

    Task<ClassifierReferenceCm> GetClassifier(int id);
    Task<MediumReferenceCm> GetMedium(int id);
    Task<PredicateReference> GetPredicate(int id);
    Task<PurposeReferenceCm> GetPurpose(int id);
    Task<UnitReference> GetUnit(int id);

    Task<ClassifierReferenceCm> CreateClassifier(ClassifierReferenceAm classifierAm);
    Task<MediumReferenceCm> CreateMedium(MediumReferenceAm mediumAm);
    Task<PredicateReference> CreatePredicate(PredicateReferenceRequest request);
    Task<PurposeReferenceCm> CreatePurpose(PurposeReferenceAm purposeAm);
    Task<UnitReference> CreateUnit(UnitReferenceRequest request);

    Task DeleteClassifier(int id);
    Task DeleteMedium(int id);
    Task DeletePredicate(int id);
    Task DeletePurpose(int id);
    Task DeleteUnit(int id);
}