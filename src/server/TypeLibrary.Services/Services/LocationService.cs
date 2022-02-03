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
    }
}