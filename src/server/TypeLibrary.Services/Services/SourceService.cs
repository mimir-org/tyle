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

        public Task<IEnumerable<SourceLibAm>> GetSources()
        {
            var dataList = _sourceRepository.GetAll();
            var dataAm = _mapper.Map<List<SourceLibAm>>(dataList);
            return Task.FromResult<IEnumerable<SourceLibAm>>(dataAm);
        }

        public async Task<SourceLibAm> UpdateSource(SourceLibAm dataAm)
        {
            var data = _mapper.Map<SourceLibDm>(dataAm);
            data.Updated = DateTime.Now.ToUniversalTime();
            data.UpdatedBy = _contextAccessor?.GetName() ?? "Unknown";
            _sourceRepository.Update(data);
            await _sourceRepository.SaveAsync();
            return _mapper.Map<SourceLibAm>(data);
        }

        public async Task<SourceLibAm> CreateSource(SourceLibAm dataAm)
        {
            var data = _mapper.Map<SourceLibDm>(dataAm);
            data.Created = DateTime.Now.ToUniversalTime();
            data.CreatedBy = _contextAccessor?.GetName() ?? "Unknown";
            data.Id = data.Key.CreateMd5();
            var createdData = await _sourceRepository.CreateAsync(data);
            await _sourceRepository.SaveAsync();
            return _mapper.Map<SourceLibAm>(createdData.Entity);
        }

        public async Task CreateSources(List<SourceLibAm> dataAm)
        {
            var dataList = _mapper.Map<List<SourceLibDm>>(dataAm);
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