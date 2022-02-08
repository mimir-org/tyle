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
    public class AttributeAspectService : IAttributeAspectService
    {
        private readonly IMapper _mapper;
        private readonly IAttributeAspectRepository _attributeAspectRepository;
        private readonly IHttpContextAccessor _contextAccessor;

        public AttributeAspectService(IMapper mapper, IAttributeAspectRepository attributeAspectRepository, IHttpContextAccessor contextAccessor)
        {
            _mapper = mapper;
            _attributeAspectRepository = attributeAspectRepository;
            _contextAccessor = contextAccessor;
        }

        public Task<IEnumerable<AttributeAspectLibAm>> GetAttributeAspects()
        {
            var dataList = _attributeAspectRepository.GetAll();
            var dataAm = _mapper.Map<List<AttributeAspectLibAm>>(dataList);
            return Task.FromResult<IEnumerable<AttributeAspectLibAm>>(dataAm);
        }

        public async Task<AttributeAspectLibAm> UpdateAttributeAspect(AttributeAspectLibAm dataAm)
        {
            var data = _mapper.Map<AttributeAspectLibDm>(dataAm);
            data.Updated = DateTime.Now.ToUniversalTime();
            data.UpdatedBy = _contextAccessor?.GetName() ?? "Unknown";
            _attributeAspectRepository.Update(data);
            await _attributeAspectRepository.SaveAsync();
            return _mapper.Map<AttributeAspectLibAm>(data);
        }

        public async Task<AttributeAspectLibAm> CreateAttributeAspect(AttributeAspectLibAm dataAm)
        {
            var data = _mapper.Map<AttributeAspectLibDm>(dataAm);
            data.Created = DateTime.Now.ToUniversalTime();
            data.CreatedBy = _contextAccessor?.GetName() ?? "Unknown";
            data.Id = data.Key.CreateMd5();
            var createdData = await _attributeAspectRepository.CreateAsync(data);
            await _attributeAspectRepository.SaveAsync();
            return _mapper.Map<AttributeAspectLibAm>(createdData.Entity);
        }

        public async Task CreateAttributeAspects(List<AttributeAspectLibAm> dataAm)
        {
            var dataList = _mapper.Map<List<AttributeAspectLibDm>>(dataAm);
            var existing = _attributeAspectRepository.GetAll().ToList();
            var notExisting = dataList.Where(x => existing.All(y => y.Id != x.Key.CreateMd5())).ToList();

            if (!notExisting.Any())
                return;

            foreach (var data in notExisting)
            {
                data.Created = DateTime.Now.ToUniversalTime();
                data.CreatedBy = _contextAccessor?.GetName() ?? "Unknown";
                data.Id = data.Key.CreateMd5();
                _attributeAspectRepository.Attach(data, EntityState.Added);
            }

            await _attributeAspectRepository.SaveAsync();

            foreach (var data in notExisting)
                _attributeAspectRepository.Detach(data);
        }
    }
}