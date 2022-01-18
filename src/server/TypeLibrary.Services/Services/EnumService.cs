using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Mimirorg.Common.Extensions;
using TypeLibrary.Models.Data;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Models.Application;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Services
{
    public class EnumService : IEnumService
    {
        private readonly IMapper _mapper;
        private readonly IConditionRepository _conditionRepository;

        public EnumService(IMapper mapper,
            IConditionRepository conditionRepository)
        {
            _mapper = mapper;
            _conditionRepository = conditionRepository;
        }

        #region Condition

        public Task<IEnumerable<ConditionAm>> GetConditions()
        {
            var conditions = _conditionRepository.GetAll();
            var conditionsAm = _mapper.Map<List<ConditionAm>>(conditions);
            return Task.FromResult<IEnumerable<ConditionAm>>(conditionsAm);
        }

        public async Task<ConditionAm> UpdateCondition(ConditionAm conditionAm)
        {
            var condition = _mapper.Map<Condition>(conditionAm);
            _conditionRepository.Update(condition);
            await _conditionRepository.SaveAsync();
            return _mapper.Map<ConditionAm>(condition);
        }

        public async Task<ConditionAm> CreateCondition(ConditionAm conditionAm)
        {
            var condition = _mapper.Map<Condition>(conditionAm);
            condition.Id = condition.Key.CreateMd5();
            var createdCondition = await _conditionRepository.CreateAsync(condition);
            await _conditionRepository.SaveAsync();
            return _mapper.Map<ConditionAm>(createdCondition.Entity);
        }

        #endregion Condition
    }
}