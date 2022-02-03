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
    public class UnitService : IUnitService
    {
        private readonly IMapper _mapper;
        private readonly IUnitRepository _unitRepository;
        private readonly IHttpContextAccessor _contextAccessor;

        public UnitService(IMapper mapper, IUnitRepository unitRepository, IHttpContextAccessor contextAccessor)
        {
            _mapper = mapper;
            _unitRepository = unitRepository;
            _contextAccessor = contextAccessor;
        }

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
    }
}