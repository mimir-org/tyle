using System.Collections.Generic;
using System.Threading.Tasks;
using TypeLibrary.Models.Models.Application;

namespace TypeLibrary.Services.Contracts
{
    public interface IEnumService
    {
        #region ConditionDm

        Task<IEnumerable<ConditionAm>> GetConditions();
        Task<ConditionAm> UpdateCondition(ConditionAm dataAm);
        Task<ConditionAm> CreateCondition(ConditionAm dataAm);
        Task CreateConditions(List<ConditionAm> dataAm);

        #endregion ConditionDm


        #region FormatDm

        Task<IEnumerable<FormatAm>> GetFormats();
        Task<FormatAm> UpdateFormat(FormatAm dataAm);
        Task<FormatAm> CreateFormat(FormatAm dataAm);
        Task CreateFormats(List<FormatAm> dataAm);

        #endregion FormatDm


        #region QualifierDm

        Task<IEnumerable<QualifierAm>> GetQualifiers();
        Task<QualifierAm> UpdateQualifier(QualifierAm dataAm);
        Task<QualifierAm> CreateQualifier(QualifierAm dataAm);
        Task CreateQualifiers(List<QualifierAm> dataAm);

        #endregion QualifierDm


        #region SourceDm

        Task<IEnumerable<SourceAm>> GetSources();
        Task<SourceAm> UpdateSource(SourceAm dataAm);
        Task<SourceAm> CreateSource(SourceAm dataAm);
        Task CreateSources(List<SourceAm> dataAm);

        #endregion SourceDm


        #region LocationDm

        Task<IEnumerable<LocationAm>> GetLocations();
        Task<LocationAm> UpdateLocation(LocationAm dataAm);
        Task<LocationAm> CreateLocation(LocationAm dataAm);
        Task CreateLocations(List<LocationAm> dataAm);

        #endregion SourceDm


        #region PurposeDm

        Task<IEnumerable<PurposeAm>> GetPurposes();
        Task<PurposeAm> UpdatePurpose(PurposeAm dataAm);
        Task<PurposeAm> CreatePurpose(PurposeAm dataAm);
        Task CreatePurposes(List<PurposeAm> dataAm);

        #endregion PurposeDm


        #region RdsCategoryDm

        Task<IEnumerable<RdsCategoryAm>> GetRdsCategories();
        Task<RdsCategoryAm> UpdateRdsCategory(RdsCategoryAm dataAm);
        Task<RdsCategoryAm> CreateRdsCategory(RdsCategoryAm dataAm);
        Task CreateRdsCategories(List<RdsCategoryAm> dataAm);
        
        #endregion RdsCategoryDm


        #region UnitDm

        Task<IEnumerable<UnitAm>> GetUnits();
        Task<UnitAm> UpdateUnit(UnitAm dataAm);
        Task<UnitAm> CreateUnit(UnitAm dataAm);
        Task CreateUnits(List<UnitAm> dataAm);

        #endregion UnitDm


        #region Collection

        Task<IEnumerable<CollectionAm>> GetCollections();
        Task<CollectionAm> UpdateCollection(CollectionAm dataAm);
        Task<CollectionAm> CreateCollection(CollectionAm dataAm);
        Task CreateCollections(List<CollectionAm> dataAm);

        #endregion Collection
    }
}