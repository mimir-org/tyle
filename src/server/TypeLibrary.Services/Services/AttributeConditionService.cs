using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Mimirorg.Common.Extensions;
using TypeLibrary.Data.Contracts;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Data;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Services
{
    public class AttributeConditionService : IAttributeConditionService
    {
        private readonly IMapper _mapper;
        private readonly IConditionRepository _conditionRepository;
        private readonly IHttpContextAccessor _contextAccessor;

        public AttributeConditionService(IMapper mapper, IConditionRepository conditionRepository, IHttpContextAccessor contextAccessor)
        {
            _mapper = mapper;
            _conditionRepository = conditionRepository;
            _contextAccessor = contextAccessor;
        }

        public Task<IEnumerable<AttributeConditionLibAm>> GetAttributeConditions()
        {
            var dataList = _conditionRepository.GetAll();
            var dataAm = _mapper.Map<List<AttributeConditionLibAm>>(dataList);
            return Task.FromResult<IEnumerable<AttributeConditionLibAm>>(dataAm);
        }

        public async Task<AttributeConditionLibAm> UpdateAttributeCondition(AttributeConditionLibAm dataAm)
        {
            var data = _mapper.Map<AttributeConditionLibDm>(dataAm);
            data.Updated = DateTime.Now.ToUniversalTime();
            data.UpdatedBy = _contextAccessor?.GetName() ?? "Unknown";
            _conditionRepository.Update(data);
            await _conditionRepository.SaveAsync();
            return _mapper.Map<AttributeConditionLibAm>(data);
        }

        public async Task<AttributeConditionLibAm> CreateAttributeCondition(AttributeConditionLibAm dataAm)
        {
            var data = _mapper.Map<AttributeConditionLibDm>(dataAm);
            data.Created = DateTime.Now.ToUniversalTime();
            data.CreatedBy = _contextAccessor?.GetName() ?? "Unknown";
            data.Id = data.Key.CreateMd5();
            var createdData = await _conditionRepository.CreateAsync(data);
            await _conditionRepository.SaveAsync();
            return _mapper.Map<AttributeConditionLibAm>(createdData.Entity);
        }

        public async Task CreateAttributeConditions(List<AttributeConditionLibAm> dataAm)
        {
            var dataList = _mapper.Map<List<AttributeConditionLibDm>>(dataAm);
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