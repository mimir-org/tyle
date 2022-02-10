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
    public class AttributeSourceService : IAttributeSourceService
    {
        private readonly IMapper _mapper;
        private readonly ISourceRepository _sourceRepository;
        private readonly IHttpContextAccessor _contextAccessor;

        public AttributeSourceService(IMapper mapper, ISourceRepository sourceRepository, IHttpContextAccessor contextAccessor)
        {
            _mapper = mapper;
            _sourceRepository = sourceRepository;
            _contextAccessor = contextAccessor;
        }

        public Task<IEnumerable<AttributeSourceLibAm>> GetAttributeSources()
        {
            var dataList = _sourceRepository.GetAll();
            var dataAm = _mapper.Map<List<AttributeSourceLibAm>>(dataList);
            return Task.FromResult<IEnumerable<AttributeSourceLibAm>>(dataAm);
        }

        public async Task<AttributeSourceLibAm> UpdateAttributeSource(AttributeSourceLibAm dataAm)
        {
            var data = _mapper.Map<AttributeSourceLibDm>(dataAm);
            data.Updated = DateTime.Now.ToUniversalTime();
            data.UpdatedBy = _contextAccessor?.GetName() ?? "Unknown";
            _sourceRepository.Update(data);
            await _sourceRepository.SaveAsync();
            return _mapper.Map<AttributeSourceLibAm>(data);
        }

        public async Task<AttributeSourceLibAm> CreateAttributeSource(AttributeSourceLibAm dataAm)
        {
            var data = _mapper.Map<AttributeSourceLibDm>(dataAm);
            data.Created = DateTime.Now.ToUniversalTime();
            data.CreatedBy = _contextAccessor?.GetName() ?? "Unknown";
            var createdData = await _sourceRepository.CreateAsync(data);
            await _sourceRepository.SaveAsync();
            return _mapper.Map<AttributeSourceLibAm>(createdData.Entity);
        }

        public async Task CreateAttributeSources(List<AttributeSourceLibAm> dataAm)
        {
            var dataList = _mapper.Map<List<AttributeSourceLibDm>>(dataAm);
            var existing = _sourceRepository.GetAll().ToList();
            var notExisting = dataList.Where(x => existing.All(y => y.Name != x.Name)).ToList();

            if (!notExisting.Any())
                return;

            foreach (var data in notExisting)
            {
                data.Created = DateTime.Now.ToUniversalTime();
                data.CreatedBy = _contextAccessor?.GetName() ?? "Unknown";
                _sourceRepository.Attach(data, EntityState.Added);
            }

            await _sourceRepository.SaveAsync();

            foreach (var data in notExisting)
                _sourceRepository.Detach(data);
        }
    }
}