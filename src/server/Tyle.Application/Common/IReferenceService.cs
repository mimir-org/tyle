using Tyle.Application.Attributes.Requests;
using Tyle.Application.Common.Requests;
using Tyle.Application.Terminals.Requests;
using Tyle.Core.Attributes;
using Tyle.Core.Common;
using Tyle.Core.Terminals;

namespace Tyle.Application.Common;

public interface IReferenceService
{
    Task<IEnumerable<ClassifierReference>> GetAllClassifiers();
    Task<IEnumerable<MediumReference>> GetAllMedia();
    Task<IEnumerable<PredicateReference>> GetAllPredicates();
    Task<IEnumerable<PurposeReference>> GetAllPurposes();
    Task<IEnumerable<UnitReference>> GetAllUnits();

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