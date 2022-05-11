using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Mimirorg.Common.Extensions;
using Mimirorg.Common.Models;
using TypeLibrary.Data.Contracts;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Models;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Services
{
    public class UnitService : IUnitService
    {
        private readonly IMapper _mapper;
        private readonly IUnitRepository _unitRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ApplicationSettings _applicationSettings;

        public UnitService(IMapper mapper, IUnitRepository unitRepository, IHttpContextAccessor contextAccessor, IOptions<ApplicationSettings> applicationSettings)
        {
            _mapper = mapper;
            _unitRepository = unitRepository;
            _contextAccessor = contextAccessor;
            _applicationSettings = applicationSettings?.Value;
        }

        public Task<IEnumerable<UnitLibCm>> GetUnits()
        {
            var dataList = _unitRepository.GetAll().Where(x => !x.Deleted).ToList()
                .OrderBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList();

            var dataAm = _mapper.Map<List<UnitLibCm>>(dataList);
            return Task.FromResult(dataAm.AsEnumerable());
        }

        public async Task<UnitLibCm> UpdateUnit(UnitLibAm dataAm, string id)
        {
            var data = _mapper.Map<UnitLibDm>(dataAm);
            data.Id = id;
            data.Updated = DateTime.Now.ToUniversalTime();
            data.UpdatedBy = _contextAccessor?.GetName() ?? "Unknown";
            _unitRepository.Update(data);
            await _unitRepository.SaveAsync();
            return _mapper.Map<UnitLibCm>(data);
        }

        public async Task<UnitLibCm> CreateUnit(UnitLibAm dataAm)
        {
            var data = _mapper.Map<UnitLibDm>(dataAm);
            var createdData = await _unitRepository.CreateAsync(data);
            await _unitRepository.SaveAsync();
            return _mapper.Map<UnitLibCm>(createdData.Entity);
        }

        public async Task CreateUnits(List<UnitLibAm> dataAm, bool createdBySystem = false)
        {
            var dataList = _mapper.Map<List<UnitLibDm>>(dataAm);
            var existing = _unitRepository.GetAll().ToList();
            var notExisting = dataList.Where(x => existing.All(y => y.Id != x.Id)).ToList();

            if (!notExisting.Any())
                return;

            foreach (var data in notExisting)
            {
                data.CreatedBy = createdBySystem ? _applicationSettings.System : data.CreatedBy;
                _unitRepository.Attach(data, EntityState.Added);
            }

            await _unitRepository.SaveAsync();

            foreach (var data in notExisting)
                _unitRepository.Detach(data);
        }
    }
}