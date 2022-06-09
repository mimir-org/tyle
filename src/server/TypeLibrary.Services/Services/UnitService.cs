using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using Mimirorg.Common.Models;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Services
{
    public class UnitService : IUnitService
    {
        private readonly IMapper _mapper;
        private readonly IUnitRepository _unitRepository;
        private readonly ApplicationSettings _applicationSettings;

        public UnitService(IMapper mapper, IUnitRepository unitRepository, IOptions<ApplicationSettings> applicationSettings)
        {
            _mapper = mapper;
            _unitRepository = unitRepository;
            _applicationSettings = applicationSettings?.Value;
        }

        public Task<IEnumerable<UnitLibCm>> GetUnits()
        {
            var dataList = _unitRepository.Get().ToList()
                .OrderBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList();

            var dataAm = _mapper.Map<List<UnitLibCm>>(dataList);
            return Task.FromResult(dataAm.AsEnumerable());
        }

        public async Task CreateUnits(List<UnitLibAm> dataAm, bool createdBySystem = false)
        {
            var dataList = _mapper.Map<List<UnitLibDm>>(dataAm);
            var existing = _unitRepository.Get().ToList();
            var notExisting = dataList.Where(x => existing.All(y => y.Id != x.Id)).ToList();

            if (!notExisting.Any())
                return;

            foreach (var data in notExisting)
                data.CreatedBy = createdBySystem ? _applicationSettings.System : data.CreatedBy;

            await _unitRepository.Create(notExisting);
            _unitRepository.ClearAllChangeTrackers();
        }
    }
}