using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using Mimirorg.TypeLibrary.Models.Domain;

namespace TypeLibrary.Services.Contracts;

public interface IReferenceService
{
    IEnumerable<ClassifierReference> GetAllClassifiers();
    IEnumerable<MediumReference> GetAllMedia();
    IEnumerable<PredicateReference> GetAllPredicates();
    IEnumerable<PurposeReference> GetAllPurposes();
    IEnumerable<UnitReference> GetAllUnits();

    Task<ClassifierReference> GetClassifier(int id);
    Task<MediumReference> GetMedium(int id);
    Task<PredicateReference> GetPredicate(int id);
    Task<PurposeReference> GetPurpose(int id);
    Task<UnitReference> GetUnit(int id);

    Task<ClassifierReference> CreateClassifier(ClassifierReferenceRequest request);
    Task<MediumReference> CreateMedium(MediumReferenceRequest request);
    Task<PredicateReference> CreatePredicate(PredicateReferenceRequest request);
    Task<PurposeReference> CreatePurpose(PurposeReferenceRequest request);
    Task<UnitReference> CreateUnit(UnitReferenceRequest request);

    Task DeleteClassifier(int id);
    Task DeleteMedium(int id);
    Task DeletePredicate(int id);
    Task DeletePurpose(int id);
    Task DeleteUnit(int id);
}