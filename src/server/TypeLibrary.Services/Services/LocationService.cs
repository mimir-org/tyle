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
    public class LocationService : ILocationService
    {
        private readonly IMapper _mapper;
        private readonly ILocationRepository _locationRepository;
        private readonly IHttpContextAccessor _contextAccessor;

        public LocationService(IMapper mapper, ILocationRepository locationRepository, IHttpContextAccessor contextAccessor)
        {
            _mapper = mapper;
            _locationRepository = locationRepository;
            _contextAccessor = contextAccessor;
        }

        public Task<IEnumerable<LocationLibAm>> GetLocations()
        {
            var dataList = _locationRepository.GetAll();
            var dataAm = _mapper.Map<List<LocationLibAm>>(dataList);
            return Task.FromResult<IEnumerable<LocationLibAm>>(dataAm);
        }

        public async Task<LocationLibAm> UpdateLocation(LocationLibAm dataAm)
        {
            var data = _mapper.Map<LocationLibDm>(dataAm);
            data.Updated = DateTime.Now.ToUniversalTime();
            data.UpdatedBy = _contextAccessor?.GetName() ?? "Unknown";
            _locationRepository.Update(data);
            await _locationRepository.SaveAsync();
            return _mapper.Map<LocationLibAm>(data);
        }

        public async Task<LocationLibAm> CreateLocation(LocationLibAm dataAm)
        {
            var data = _mapper.Map<LocationLibDm>(dataAm);
            data.Created = DateTime.Now.ToUniversalTime();
            data.CreatedBy = _contextAccessor?.GetName() ?? "Unknown";
            data.Id = data.Key.CreateMd5();
            var createdData = await _locationRepository.CreateAsync(data);
            await _locationRepository.SaveAsync();
            return _mapper.Map<LocationLibAm>(createdData.Entity);
        }

        public async Task CreateLocations(List<LocationLibAm> dataAm)
        {
            var dataList = _mapper.Map<List<LocationLibDm>>(dataAm);
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
    }
}