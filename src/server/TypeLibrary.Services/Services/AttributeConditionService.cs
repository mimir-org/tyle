using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Mimirorg.Common.Extensions;
using Mimirorg.TypeLibrary.Enums;
using TypeLibrary.Data.Contracts;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Models;
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

        public Task<IEnumerable<AttributeConditionLibCm>> GetAttributeConditions()
        {
            var notSet = _conditionRepository.FindBy(x => x.Name == Aspect.NotSet.ToString())?.First();
            var dataSet = _conditionRepository.GetAll().Where(x => x.Name != Aspect.NotSet.ToString()).ToList();

            var dataDmList = new List<AttributeConditionLibDm> { notSet };
            dataDmList.AddRange(dataSet.OrderBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList());

            var dataCmList = _mapper.Map<List<AttributeConditionLibCm>>(dataDmList);
            return Task.FromResult(dataCmList.AsEnumerable());
        }

        public async Task<AttributeConditionLibCm> UpdateAttributeCondition(AttributeConditionLibAm dataAm, int id)
        {
            var data = _mapper.Map<AttributeConditionLibDm>(dataAm);
            data.Id = id;
            data.Updated = DateTime.Now.ToUniversalTime();
            data.UpdatedBy = _contextAccessor?.GetName() ?? "Unknown";
            _conditionRepository.Update(data);
            await _conditionRepository.SaveAsync();
            return _mapper.Map<AttributeConditionLibCm>(data);
        }

        public async Task<AttributeConditionLibCm> CreateAttributeCondition(AttributeConditionLibAm dataAm)
        {
            var data = _mapper.Map<AttributeConditionLibDm>(dataAm);
            data.Created = DateTime.Now.ToUniversalTime();
            data.CreatedBy = _contextAccessor?.GetName() ?? "Unknown";
            var createdData = await _conditionRepository.CreateAsync(data);
            await _conditionRepository.SaveAsync();
            return _mapper.Map<AttributeConditionLibCm>(createdData.Entity);
        }

        public async Task CreateAttributeConditions(List<AttributeConditionLibAm> dataAm)
        {
            var dataList = _mapper.Map<List<AttributeConditionLibDm>>(dataAm);
            var existing = _conditionRepository.GetAll().ToList();
            var notExisting = dataList.Where(x => existing.All(y => y.Name != x.Name)).ToList();

            if (!notExisting.Any())
                return;

            foreach (var data in notExisting)
            {
                data.Created = DateTime.Now.ToUniversalTime();
                data.CreatedBy = _contextAccessor?.GetName() ?? "Unknown";
                _conditionRepository.Attach(data, EntityState.Added);
            }

            await _conditionRepository.SaveAsync();

            foreach (var data in notExisting)
                _conditionRepository.Detach(data);
        }
    }
}