using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Mimirorg.Common.Extensions;

using TypeLibrary.Data.Contracts;
using TypeLibrary.Models.Models.Application;
using TypeLibrary.Models.Models.Data;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Services
{
    public class EnumService : IEnumService
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
        private readonly ICollectionRepository _collectionRepository;
        private readonly IHttpContextAccessor _contextAccessor;

        public EnumService(IMapper mapper, IConditionRepository conditionRepository, IFormatRepository formatRepository,
            IQualifierRepository qualifierRepository, ISourceRepository sourceRepository, ILocationRepository locationRepository,
            IPurposeRepository purposeRepository, IRdsCategoryRepository rdsCategoryRepository, IUnitRepository unitRepository, 
            ICollectionRepository collectionRepository, IHttpContextAccessor contextAccessor)
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
            _collectionRepository = collectionRepository;
            _contextAccessor = contextAccessor;
        }

        #region ConditionDm

        public Task<IEnumerable<ConditionAm>> GetConditions()
        {
            var dataList = _conditionRepository.GetAll();
            var dataAm = _mapper.Map<List<ConditionAm>>(dataList);
            return Task.FromResult<IEnumerable<ConditionAm>>(dataAm);
        }

        public async Task<ConditionAm> UpdateCondition(ConditionAm dataAm)
        {
            var data = _mapper.Map<ConditionDm>(dataAm);
            data.Updated = DateTime.Now.ToUniversalTime();
            data.UpdatedBy = _contextAccessor?.GetName() ?? "Unknown";
            _conditionRepository.Update(data);
            await _conditionRepository.SaveAsync();
            return _mapper.Map<ConditionAm>(data);
        }

        public async Task<ConditionAm> CreateCondition(ConditionAm dataAm)
        {
            var data = _mapper.Map<ConditionDm>(dataAm);
            data.Created = DateTime.Now.ToUniversalTime();
            data.CreatedBy = _contextAccessor?.GetName() ?? "Unknown";
            data.Id = data.Key.CreateMd5();
            var createdData = await _conditionRepository.CreateAsync(data);
            await _conditionRepository.SaveAsync();
            return _mapper.Map<ConditionAm>(createdData.Entity);
        }

        public async Task CreateConditions(List<ConditionAm> dataAm)
        {
            var dataList = _mapper.Map<List<ConditionDm>>(dataAm);
            var existing = _conditionRepository.GetAll().ToList();
            var notExisting = dataList.Where(x => existing.All(y => y.Id != x.Key.CreateMd5())).ToList();

            if (!notExisting.Any())
                return;

            foreach (var data in notExisting)
            {
                data.Created = DateTime.Now.ToUniversalTime();
                data.CreatedBy = _contextAccessor?.GetName() ?? "Unknown";
                data.Id = data.Key.CreateMd5();
                _conditionRepository.Attach(data, EntityState.Added);
            }

            await _conditionRepository.SaveAsync();

            foreach (var data in notExisting)
                _conditionRepository.Detach(data);
        }

        #endregion ConditionDm


        #region FormatDm

        public Task<IEnumerable<FormatAm>> GetFormats()
        {
            var dataList = _formatRepository.GetAll();
            var dataAm = _mapper.Map<List<FormatAm>>(dataList);
            return Task.FromResult<IEnumerable<FormatAm>>(dataAm);
        }

        public async Task<FormatAm> UpdateFormat(FormatAm dataAm)
        {
            var data = _mapper.Map<FormatDm>(dataAm);
            data.Updated = DateTime.Now.ToUniversalTime();
            data.UpdatedBy = _contextAccessor?.GetName() ?? "Unknown";
            _formatRepository.Update(data);
            await _formatRepository.SaveAsync();
            return _mapper.Map<FormatAm>(data);
        }

        public async Task<FormatAm> CreateFormat(FormatAm dataAm)
        {
            var data = _mapper.Map<FormatDm>(dataAm);
            data.Created = DateTime.Now.ToUniversalTime();
            data.CreatedBy = _contextAccessor?.GetName() ?? "Unknown";
            data.Id = data.Key.CreateMd5();
            var createdData = await _formatRepository.CreateAsync(data);
            await _formatRepository.SaveAsync();
            return _mapper.Map<FormatAm>(createdData.Entity);
        }

        public async Task CreateFormats(List<FormatAm> dataAm)
        {
            var dataList = _mapper.Map<List<FormatDm>>(dataAm);
            var existing = _formatRepository.GetAll().ToList();
            var notExisting = dataList.Where(x => existing.All(y => y.Id != x.Key.CreateMd5())).ToList();

            if (!notExisting.Any())
                return;

            foreach (var data in notExisting)
            {
                data.Created = DateTime.Now.ToUniversalTime();
                data.CreatedBy = _contextAccessor?.GetName() ?? "Unknown";
                data.Id = data.Key.CreateMd5();
                _formatRepository.Attach(data, EntityState.Added);
            }

            await _formatRepository.SaveAsync();

            foreach (var data in notExisting)
                _formatRepository.Detach(data);
        }

        #endregion FormatDm


        #region QualifierDm

        public Task<IEnumerable<QualifierAm>> GetQualifiers()
        {
            var dataList = _qualifierRepository.GetAll();
            var dataAm = _mapper.Map<List<QualifierAm>>(dataList);
            return Task.FromResult<IEnumerable<QualifierAm>>(dataAm);
        }

        public async Task<QualifierAm> UpdateQualifier(QualifierAm dataAm)
        {
            var data = _mapper.Map<QualifierDm>(dataAm);
            data.Updated = DateTime.Now.ToUniversalTime();
            data.UpdatedBy = _contextAccessor?.GetName() ?? "Unknown";
            _qualifierRepository.Update(data);
            await _qualifierRepository.SaveAsync();
            return _mapper.Map<QualifierAm>(data);
        }

        public async Task<QualifierAm> CreateQualifier(QualifierAm dataAm)
        {
            var data = _mapper.Map<QualifierDm>(dataAm);
            data.Created = DateTime.Now.ToUniversalTime();
            data.CreatedBy = _contextAccessor?.GetName() ?? "Unknown";
            data.Id = data.Key.CreateMd5();
            var createdData = await _qualifierRepository.CreateAsync(data);
            await _qualifierRepository.SaveAsync();
            return _mapper.Map<QualifierAm>(createdData.Entity);
        }

        public async Task CreateQualifiers(List<QualifierAm> dataAm)
        {
            var dataList = _mapper.Map<List<QualifierDm>>(dataAm);
            var existing = _qualifierRepository.GetAll().ToList();
            var notExisting = dataList.Where(x => existing.All(y => y.Id != x.Key.CreateMd5())).ToList();

            if (!notExisting.Any())
                return;

            foreach (var data in notExisting)
            {
                data.Created = DateTime.Now.ToUniversalTime();
                data.CreatedBy = _contextAccessor?.GetName() ?? "Unknown";
                data.Id = data.Key.CreateMd5();
                _qualifierRepository.Attach(data, EntityState.Added);
            }

            await _qualifierRepository.SaveAsync();

            foreach (var data in notExisting)
                _qualifierRepository.Detach(data);
        }

        #endregion QualifierDm


        #region SourceDm

        public Task<IEnumerable<SourceAm>> GetSources()
        {
            var dataList = _sourceRepository.GetAll();
            var dataAm = _mapper.Map<List<SourceAm>>(dataList);
            return Task.FromResult<IEnumerable<SourceAm>>(dataAm);
        }

        public async Task<SourceAm> UpdateSource(SourceAm dataAm)
        {
            var data = _mapper.Map<SourceDm>(dataAm);
            data.Updated = DateTime.Now.ToUniversalTime();
            data.UpdatedBy = _contextAccessor?.GetName() ?? "Unknown";
            _sourceRepository.Update(data);
            await _sourceRepository.SaveAsync();
            return _mapper.Map<SourceAm>(data);
        }

        public async Task<SourceAm> CreateSource(SourceAm dataAm)
        {
            var data = _mapper.Map<SourceDm>(dataAm);
            data.Created = DateTime.Now.ToUniversalTime();
            data.CreatedBy = _contextAccessor?.GetName() ?? "Unknown";
            data.Id = data.Key.CreateMd5();
            var createdData = await _sourceRepository.CreateAsync(data);
            await _sourceRepository.SaveAsync();
            return _mapper.Map<SourceAm>(createdData.Entity);
        }

        public async Task CreateSources(List<SourceAm> dataAm)
        {
            var dataList = _mapper.Map<List<SourceDm>>(dataAm);
            var existing = _sourceRepository.GetAll().ToList();
            var notExisting = dataList.Where(x => existing.All(y => y.Id != x.Key.CreateMd5())).ToList();

            if (!notExisting.Any())
                return;

            foreach (var data in notExisting)
            {
                data.Created = DateTime.Now.ToUniversalTime();
                data.CreatedBy = _contextAccessor?.GetName() ?? "Unknown";
                data.Id = data.Key.CreateMd5();
                _sourceRepository.Attach(data, EntityState.Added);
            }

            await _sourceRepository.SaveAsync();

            foreach (var data in notExisting)
                _sourceRepository.Detach(data);
        }

        #endregion SourceDm


        #region LocationDm

        public Task<IEnumerable<LocationAm>> GetLocations()
        {
            var dataList = _locationRepository.GetAll();
            var dataAm = _mapper.Map<List<LocationAm>>(dataList);
            return Task.FromResult<IEnumerable<LocationAm>>(dataAm);
        }

        public async Task<LocationAm> UpdateLocation(LocationAm dataAm)
        {
            var data = _mapper.Map<LocationDm>(dataAm);
            data.Updated = DateTime.Now.ToUniversalTime();
            data.UpdatedBy = _contextAccessor?.GetName() ?? "Unknown";
            _locationRepository.Update(data);
            await _locationRepository.SaveAsync();
            return _mapper.Map<LocationAm>(data);
        }

        public async Task<LocationAm> CreateLocation(LocationAm dataAm)
        {
            var data = _mapper.Map<LocationDm>(dataAm);
            data.Created = DateTime.Now.ToUniversalTime();
            data.CreatedBy = _contextAccessor?.GetName() ?? "Unknown";
            data.Id = data.Key.CreateMd5();
            var createdData = await _locationRepository.CreateAsync(data);
            await _locationRepository.SaveAsync();
            return _mapper.Map<LocationAm>(createdData.Entity);
        }

        public async Task CreateLocations(List<LocationAm> dataAm)
        {
            var dataList = _mapper.Map<List<LocationDm>>(dataAm);
            var existing = _locationRepository.GetAll().ToList();
            var notExisting = dataList.Where(x => existing.All(y => y.Id != x.Key.CreateMd5())).ToList();

            if (!notExisting.Any())
                return;

            foreach (var data in notExisting)
            {
                data.Created = DateTime.Now.ToUniversalTime();
                data.CreatedBy = _contextAccessor?.GetName() ?? "Unknown";
                data.Id = data.Key.CreateMd5();
                _locationRepository.Attach(data, EntityState.Added);
            }

            await _locationRepository.SaveAsync();

            foreach (var data in notExisting)
                _locationRepository.Detach(data);
        }

        #endregion LocationDm


        #region PurposeDm

        public Task<IEnumerable<PurposeAm>> GetPurposes()
        {
            var dataList = _purposeRepository.GetAll();
            var dataAm = _mapper.Map<List<PurposeAm>>(dataList);
            return Task.FromResult<IEnumerable<PurposeAm>>(dataAm);
        }

        public async Task<PurposeAm> UpdatePurpose(PurposeAm dataAm)
        {
            var data = _mapper.Map<PurposeDm>(dataAm);
            data.Updated = DateTime.Now.ToUniversalTime();
            data.UpdatedBy = _contextAccessor?.GetName() ?? "Unknown";
            _purposeRepository.Update(data);
            await _purposeRepository.SaveAsync();
            return _mapper.Map<PurposeAm>(data);
        }

        public async Task<PurposeAm> CreatePurpose(PurposeAm dataAm)
        {
            var data = _mapper.Map<PurposeDm>(dataAm);
            data.Created = DateTime.Now.ToUniversalTime();
            data.CreatedBy = _contextAccessor?.GetName() ?? "Unknown";
            data.Id = data.Key.CreateMd5();
            var createdData = await _purposeRepository.CreateAsync(data);
            await _purposeRepository.SaveAsync();
            return _mapper.Map<PurposeAm>(createdData.Entity);
        }

        public async Task CreatePurposes(List<PurposeAm> dataAm)
        {
            var dataList = _mapper.Map<List<PurposeDm>>(dataAm);
            var existing = _purposeRepository.GetAll().ToList();
            var notExisting = dataList.Where(x => existing.All(y => y.Id != x.Key.CreateMd5())).ToList();

            if (!notExisting.Any())
                return;

            foreach (var data in notExisting)
            {
                data.Created = DateTime.Now.ToUniversalTime();
                data.CreatedBy = _contextAccessor?.GetName() ?? "Unknown";
                data.Id = data.Key.CreateMd5();
                _purposeRepository.Attach(data, EntityState.Added);
            }

            await _purposeRepository.SaveAsync();

            foreach (var data in notExisting)
                _purposeRepository.Detach(data);
        }

        #endregion PurposeDm


        #region RdsCategoryDm

        public Task<IEnumerable<RdsCategoryAm>> GetRdsCategories()
        {
            var dataList = _rdsCategoryRepository.GetAll();
            var dataAm = _mapper.Map<List<RdsCategoryAm>>(dataList);
            return Task.FromResult<IEnumerable<RdsCategoryAm>>(dataAm);
        }

        public async Task<RdsCategoryAm> UpdateRdsCategory(RdsCategoryAm dataAm)
        {
            var data = _mapper.Map<RdsCategoryDm>(dataAm);
            data.Updated = DateTime.Now.ToUniversalTime();
            data.UpdatedBy = _contextAccessor?.GetName() ?? "Unknown";
            _rdsCategoryRepository.Update(data);
            await _rdsCategoryRepository.SaveAsync();
            return _mapper.Map<RdsCategoryAm>(data);
        }

        public async Task<RdsCategoryAm> CreateRdsCategory(RdsCategoryAm dataAm)
        {
            var data = _mapper.Map<RdsCategoryDm>(dataAm);
            data.Created = DateTime.Now.ToUniversalTime();
            data.CreatedBy = _contextAccessor?.GetName() ?? "Unknown";
            data.Id = data.Key.CreateMd5();
            var createdData = await _rdsCategoryRepository.CreateAsync(data);
            await _rdsCategoryRepository.SaveAsync();
            return _mapper.Map<RdsCategoryAm>(createdData.Entity);
        }

        public async Task CreateRdsCategories(List<RdsCategoryAm> dataAm)
        {
            var dataList = _mapper.Map<List<RdsCategoryDm>>(dataAm);
            var existing = _rdsCategoryRepository.GetAll().ToList();
            var notExisting = dataList.Where(x => existing.All(y => y.Id != x.Key.CreateMd5())).ToList();

            if (!notExisting.Any())
                return;

            foreach (var data in notExisting)
            {
                data.Created = DateTime.Now.ToUniversalTime();
                data.CreatedBy = _contextAccessor?.GetName() ?? "Unknown";
                data.Id = data.Key.CreateMd5();
                _rdsCategoryRepository.Attach(data, EntityState.Added);
            }

            await _rdsCategoryRepository.SaveAsync();

            foreach (var data in notExisting)
                _rdsCategoryRepository.Detach(data);
        }

        #endregion RdsCategoryDm


        #region UnitDm

        public Task<IEnumerable<UnitAm>> GetUnits()
        {
            var dataList = _unitRepository.GetAll();
            var dataAm = _mapper.Map<List<UnitAm>>(dataList);
            return Task.FromResult<IEnumerable<UnitAm>>(dataAm);
        }

        public async Task<UnitAm> UpdateUnit(UnitAm dataAm)
        {
            var data = _mapper.Map<UnitDm>(dataAm);
            data.Updated = DateTime.Now.ToUniversalTime();
            data.UpdatedBy = _contextAccessor?.GetName() ?? "Unknown";
            _unitRepository.Update(data);
            await _unitRepository.SaveAsync();
            return _mapper.Map<UnitAm>(data);
        }

        public async Task<UnitAm> CreateUnit(UnitAm dataAm)
        {
            var data = _mapper.Map<UnitDm>(dataAm);
            data.Created = DateTime.Now.ToUniversalTime();
            data.CreatedBy = _contextAccessor?.GetName() ?? "Unknown";
            data.Id = data.Key.CreateMd5();
            var createdData = await _unitRepository.CreateAsync(data);
            await _unitRepository.SaveAsync();
            return _mapper.Map<UnitAm>(createdData.Entity);
        }

        public async Task CreateUnits(List<UnitAm> dataAm)
        {
            var dataList = _mapper.Map<List<UnitDm>>(dataAm);
            var existing = _unitRepository.GetAll().ToList();
            var notExisting = dataList.Where(x => existing.All(y => y.Id != x.Key.CreateMd5())).ToList();

            if (!notExisting.Any())
                return;

            foreach (var data in notExisting)
            {
                data.Created = DateTime.Now.ToUniversalTime();
                data.CreatedBy = _contextAccessor?.GetName() ?? "Unknown";
                data.Id = data.Key.CreateMd5();
                _unitRepository.Attach(data, EntityState.Added);
            }

            await _unitRepository.SaveAsync();

            foreach (var data in notExisting)
                _unitRepository.Detach(data);
        }

        #endregion UnitDm


        #region CategoryDm

        public Task<IEnumerable<CollectionAm>> GetCollections()
        {
            var dataList = _collectionRepository.GetAll();
            var dataAm = _mapper.Map<List<CollectionAm>>(dataList);
            return Task.FromResult<IEnumerable<CollectionAm>>(dataAm);
        }

        public async Task<CollectionAm> UpdateCollection(CollectionAm dataAm)
        {
            var data = _mapper.Map<CollectionDm>(dataAm);
            data.Updated = DateTime.Now.ToUniversalTime();
            data.UpdatedBy = _contextAccessor?.GetName() ?? "Unknown";
            _collectionRepository.Update(data);
            await _collectionRepository.SaveAsync();
            return _mapper.Map<CollectionAm>(data);
        }

        public async Task<CollectionAm> CreateCollection(CollectionAm dataAm)
        {
            var data = _mapper.Map<CollectionDm>(dataAm);
            data.Created = DateTime.Now.ToUniversalTime();
            data.CreatedBy = _contextAccessor?.GetName() ?? "Unknown";
            data.Id = data.Key.CreateMd5();
            var createdData = await _collectionRepository.CreateAsync(data);
            await _collectionRepository.SaveAsync();
            return _mapper.Map<CollectionAm>(createdData.Entity);
        }

        public async Task CreateCollections(List<CollectionAm> dataAm)
        {
            var dataList = _mapper.Map<List<CollectionDm>>(dataAm);
            var existing = _collectionRepository.GetAll().ToList();
            var notExisting = dataList.Where(x => existing.All(y => y.Id != x.Key.CreateMd5())).ToList();

            if (!notExisting.Any())
                return;

            foreach (var data in notExisting)
            {
                data.Created = DateTime.Now.ToUniversalTime();
                data.CreatedBy = _contextAccessor?.GetName() ?? "Unknown";
                data.Id = data.Key.CreateMd5();
                _collectionRepository.Attach(data, EntityState.Added);
            }

            await _collectionRepository.SaveAsync();

            foreach (var data in notExisting)
                _collectionRepository.Detach(data);
        }

        #endregion CategoryDm
    }
}