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
    public class SourceService : ISourceService
    {
        private readonly IMapper _mapper;
        private readonly ISourceRepository _sourceRepository;
        private readonly IHttpContextAccessor _contextAccessor;

        public SourceService(IMapper mapper, ISourceRepository sourceRepository, IHttpContextAccessor contextAccessor)
        {
            _mapper = mapper;
            _sourceRepository = sourceRepository;
            _contextAccessor = contextAccessor;
        }

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
    }
}