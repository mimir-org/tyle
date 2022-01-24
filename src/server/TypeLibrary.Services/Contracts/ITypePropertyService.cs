using System.Collections.Generic;
using System.Threading.Tasks;
using TypeLibrary.Models.Application;

namespace TypeLibrary.Services.Contracts
{
    public interface ITypePropertyService
    {
        #region Condition

        Task<IEnumerable<ConditionAm>> GetConditions();
        Task<ConditionAm> UpdateCondition(ConditionAm dataAm);
        Task<ConditionAm> CreateCondition(ConditionAm dataAm);
        Task CreateConditions(List<ConditionAm> dataAm);

        #endregion Condition


        #region Format

        Task<IEnumerable<FormatAm>> GetFormats();
        Task<FormatAm> UpdateFormat(FormatAm dataAm);
        Task<FormatAm> CreateFormat(FormatAm dataAm);
        Task CreateFormats(List<FormatAm> dataAm);

        #endregion Format


        #region Qualifier

        Task<IEnumerable<QualifierAm>> GetQualifiers();
        Task<QualifierAm> UpdateQualifier(QualifierAm dataAm);
        Task<QualifierAm> CreateQualifier(QualifierAm dataAm);
        Task CreateQualifiers(List<QualifierAm> dataAm);

        #endregion Qualifier


        #region Source

        Task<IEnumerable<SourceAm>> GetSources();
        Task<SourceAm> UpdateSource(SourceAm dataAm);
        Task<SourceAm> CreateSource(SourceAm dataAm);
        Task CreateSources(List<SourceAm> dataAm);

        #endregion Source


        #region Location

        Task<IEnumerable<LocationAm>> GetLocations();
        Task<LocationAm> UpdateLocation(LocationAm dataAm);
        Task<LocationAm> CreateLocation(LocationAm dataAm);
        Task CreateLocations(List<LocationAm> dataAm);

        #endregion Source


        #region Purpose

        Task<IEnumerable<PurposeAm>> GetPurposes();
        Task<PurposeAm> UpdatePurpose(PurposeAm dataAm);
        Task<PurposeAm> CreatePurpose(PurposeAm dataAm);
        Task CreatePurposes(List<PurposeAm> dataAm);

        #endregion Purpose


        #region RdsCategory

        Task<IEnumerable<RdsCategoryAm>> GetRdsCategories();
        Task<RdsCategoryAm> UpdateRdsCategory(RdsCategoryAm dataAm);
        Task<RdsCategoryAm> CreateRdsCategory(RdsCategoryAm dataAm);
        Task CreateRdsCategories(List<RdsCategoryAm> dataAm);
        
        #endregion RdsCategory


        #region Unit

        Task<IEnumerable<UnitAm>> GetUnits();
        Task<UnitAm> UpdateUnit(UnitAm dataAm);
        Task<UnitAm> CreateUnit(UnitAm dataAm);
        Task CreateUnits(List<UnitAm> dataAm);

        #endregion Unit
    }
}