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
    public class ConditionService : IConditionService
    {
        private readonly IMapper _mapper;
        private readonly IConditionRepository _conditionRepository;
        private readonly IHttpContextAccessor _contextAccessor;

        public ConditionService(IMapper mapper, IConditionRepository conditionRepository, IHttpContextAccessor contextAccessor)
        {
            _mapper = mapper;
            _conditionRepository = conditionRepository;
            _contextAccessor = contextAccessor;
        }

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
    }
}