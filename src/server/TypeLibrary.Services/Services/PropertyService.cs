using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Mimirorg.Common.Extensions;
using TypeLibrary.Models.Data;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Models.Application;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly IMapper _mapper;
        private readonly IConditionRepository _conditionRepository;
        private readonly IFormatRepository _formatRepository;
        private readonly IQualifierRepository _qualifierRepository;
        private readonly ISourceRepository _sourceRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IPurposeRepository _purposeRepository;
        private readonly IRdsCategoryRepository _rdsCategoryRepository;
        private readonly IUnitRepository _unitRepository;

        public PropertyService(IMapper mapper, IConditionRepository conditionRepository, IFormatRepository formatRepository,
            IQualifierRepository qualifierRepository, ISourceRepository sourceRepository, ILocationRepository locationRepository,
            IPurposeRepository purposeRepository, IRdsCategoryRepository rdsCategoryRepository, IUnitRepository unitRepository)
        {
            _mapper = mapper;
            _conditionRepository = conditionRepository;
            _formatRepository = formatRepository;
            _qualifierRepository = qualifierRepository;
            _sourceRepository = sourceRepository;
            _locationRepository = locationRepository;
            _purposeRepository = purposeRepository;
            _rdsCategoryRepository = rdsCategoryRepository;
            _unitRepository = unitRepository;
        }

        #region Condition

        public Task<IEnumerable<ConditionAm>> GetConditions()
        {
            var dataList = _conditionRepository.GetAll();
            var dataAm = _mapper.Map<List<ConditionAm>>(dataList);
            return Task.FromResult<IEnumerable<ConditionAm>>(dataAm);
        }

        public async Task<ConditionAm> UpdateCondition(ConditionAm dataAm)
        {
            var data = _mapper.Map<Condition>(dataAm);
            _conditionRepository.Update(data);
            await _conditionRepository.SaveAsync();
            return _mapper.Map<ConditionAm>(data);
        }

        public async Task<ConditionAm> CreateCondition(ConditionAm dataAm)
        {
            var data = _mapper.Map<Condition>(dataAm);
            data.Id = data.Key.CreateMd5();
            var createdData = await _conditionRepository.CreateAsync(data);
            await _conditionRepository.SaveAsync();
            return _mapper.Map<ConditionAm>(createdData.Entity);
        }

        #endregion Condition


        #region Format

        public Task<IEnumerable<FormatAm>> GetFormats()
        {
            var dataList = _formatRepository.GetAll();
            var dataAm = _mapper.Map<List<FormatAm>>(dataList);
            return Task.FromResult<IEnumerable<FormatAm>>(dataAm);
        }

        public async Task<FormatAm> UpdateFormat(FormatAm dataAm)
        {
            var data = _mapper.Map<Format>(dataAm);
            _formatRepository.Update(data);
            await _formatRepository.SaveAsync();
            return _mapper.Map<FormatAm>(data);
        }

        public async Task<FormatAm> CreateFormat(FormatAm dataAm)
        {
            var data = _mapper.Map<Format>(dataAm);
            data.Id = data.Key.CreateMd5();
            var createdData = await _formatRepository.CreateAsync(data);
            await _formatRepository.SaveAsync();
            return _mapper.Map<FormatAm>(createdData.Entity);
        }

        #endregion Format


        #region Qualifier

        public Task<IEnumerable<QualifierAm>> GetQualifiers()
        {
            var dataList = _qualifierRepository.GetAll();
            var dataAm = _mapper.Map<List<QualifierAm>>(dataList);
            return Task.FromResult<IEnumerable<QualifierAm>>(dataAm);
        }

        public async Task<QualifierAm> UpdateQualifier(QualifierAm dataAm)
        {
            var data = _mapper.Map<Qualifier>(dataAm);
            _qualifierRepository.Update(data);
            await _qualifierRepository.SaveAsync();
            return _mapper.Map<QualifierAm>(data);
        }

        public async Task<QualifierAm> CreateQualifier(QualifierAm dataAm)
        {
            var data = _mapper.Map<Qualifier>(dataAm);
            data.Id = data.Key.CreateMd5();
            var createdData = await _qualifierRepository.CreateAsync(data);
            await _qualifierRepository.SaveAsync();
            return _mapper.Map<QualifierAm>(createdData.Entity);
        }

        #endregion Qualifier


        #region Source

        public Task<IEnumerable<SourceAm>> GetSources()
        {
            var dataList = _sourceRepository.GetAll();
            var dataAm = _mapper.Map<List<SourceAm>>(dataList);
            return Task.FromResult<IEnumerable<SourceAm>>(dataAm);
        }

        public async Task<SourceAm> UpdateSource(SourceAm dataAm)
        {
            var data = _mapper.Map<Source>(dataAm);
            _sourceRepository.Update(data);
            await _sourceRepository.SaveAsync();
            return _mapper.Map<SourceAm>(data);
        }

        public async Task<SourceAm> CreateSource(SourceAm dataAm)
        {
            var data = _mapper.Map<Source>(dataAm);
            data.Id = data.Key.CreateMd5();
            var createdData = await _sourceRepository.CreateAsync(data);
            await _sourceRepository.SaveAsync();
            return _mapper.Map<SourceAm>(createdData.Entity);
        }

        #endregion Source


        #region Location

        public Task<IEnumerable<LocationAm>> GetLocations()
        {
            var dataList = _locationRepository.GetAll();
            var dataAm = _mapper.Map<List<LocationAm>>(dataList);
            return Task.FromResult<IEnumerable<LocationAm>>(dataAm);
        }

        public async Task<LocationAm> UpdateLocation(LocationAm dataAm)
        {
            var data = _mapper.Map<Location>(dataAm);
            _locationRepository.Update(data);
            await _locationRepository.SaveAsync();
            return _mapper.Map<LocationAm>(data);
        }

        public async Task<LocationAm> CreateLocation(LocationAm dataAm)
        {
            var data = _mapper.Map<Location>(dataAm);
            data.Id = data.Key.CreateMd5();
            var createdData = await _locationRepository.CreateAsync(data);
            await _locationRepository.SaveAsync();
            return _mapper.Map<LocationAm>(createdData.Entity);
        }

        #endregion Location


        #region Purpose

        public Task<IEnumerable<PurposeAm>> GetPurposes()
        {
            var dataList = _purposeRepository.GetAll();
            var dataAm = _mapper.Map<List<PurposeAm>>(dataList);
            return Task.FromResult<IEnumerable<PurposeAm>>(dataAm);
        }

        public async Task<PurposeAm> UpdatePurpose(PurposeAm dataAm)
        {
            var data = _mapper.Map<Purpose>(dataAm);
            _purposeRepository.Update(data);
            await _purposeRepository.SaveAsync();
            return _mapper.Map<PurposeAm>(data);
        }

        public async Task<PurposeAm> CreatePurpose(PurposeAm dataAm)
        {
            var data = _mapper.Map<Purpose>(dataAm);
            data.Id = data.Key.CreateMd5();
            var createdData = await _purposeRepository.CreateAsync(data);
            await _purposeRepository.SaveAsync();
            return _mapper.Map<PurposeAm>(createdData.Entity);
        }

        #endregion Purpose


        #region RdsCategory

        public Task<IEnumerable<RdsCategoryAm>> GetRdsCategories()
        {
            var dataList = _rdsCategoryRepository.GetAll();
            var dataAm = _mapper.Map<List<RdsCategoryAm>>(dataList);
            return Task.FromResult<IEnumerable<RdsCategoryAm>>(dataAm);
        }

        public async Task<RdsCategoryAm> UpdateRdsCategory(RdsCategoryAm dataAm)
        {
            var data = _mapper.Map<RdsCategory>(dataAm);
            _rdsCategoryRepository.Update(data);
            await _rdsCategoryRepository.SaveAsync();
            return _mapper.Map<RdsCategoryAm>(data);
        }

        public async Task<RdsCategoryAm> CreateRdsCategory(RdsCategoryAm dataAm)
        {
            var data = _mapper.Map<RdsCategory>(dataAm);
            data.Id = data.Key.CreateMd5();
            var createdData = await _rdsCategoryRepository.CreateAsync(data);
            await _rdsCategoryRepository.SaveAsync();
            return _mapper.Map<RdsCategoryAm>(createdData.Entity);
        }

        #endregion RdsCategory


        #region Unit

        public Task<IEnumerable<UnitAm>> GetUnits()
        {
            var dataList = _unitRepository.GetAll();
            var dataAm = _mapper.Map<List<UnitAm>>(dataList);
            return Task.FromResult<IEnumerable<UnitAm>>(dataAm);
        }

        public async Task<UnitAm> UpdateUnit(UnitAm dataAm)
        {
            var data = _mapper.Map<Unit>(dataAm);
            _unitRepository.Update(data);
            await _unitRepository.SaveAsync();
            return _mapper.Map<UnitAm>(data);
        }

        public async Task<UnitAm> CreateUnit(UnitAm dataAm)
        {
            var data = _mapper.Map<Unit>(dataAm);
            data.Id = data.Key.CreateMd5();
            var createdData = await _unitRepository.CreateAsync(data);
            await _unitRepository.SaveAsync();
            return _mapper.Map<UnitAm>(createdData.Entity);
        }

        #endregion Unit
    }
}