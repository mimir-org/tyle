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
    public class AttributeTypeService : IAttributeTypeService
    {
        private readonly IMapper _mapper;
        private readonly IAttributeTypeRepository _attributeTypeRepository;
        private readonly IHttpContextAccessor _contextAccessor;

        public AttributeTypeService(IMapper mapper, IAttributeTypeRepository attributeTypeRepository, IHttpContextAccessor contextAccessor)
        {
            _mapper = mapper;
            _attributeTypeRepository = attributeTypeRepository;
            _contextAccessor = contextAccessor;
        }

        public Task<IEnumerable<AttributeTypeLibAm>> GetAttributeTypes()
        {
            var dataList = _attributeTypeRepository.GetAll();
            var dataAm = _mapper.Map<List<AttributeTypeLibAm>>(dataList);
            return Task.FromResult<IEnumerable<AttributeTypeLibAm>>(dataAm);
        }

        public async Task<AttributeTypeLibAm> UpdateAttributeType(AttributeTypeLibAm dataAm)
        {
            var data = _mapper.Map<AttributeTypeLibDm>(dataAm);
            data.Updated = DateTime.Now.ToUniversalTime();
            data.UpdatedBy = _contextAccessor?.GetName() ?? "Unknown";
            _attributeTypeRepository.Update(data);
            await _attributeTypeRepository.SaveAsync();
            return _mapper.Map<AttributeTypeLibAm>(data);
        }

        public async Task<AttributeTypeLibAm> CreateAttributeType(AttributeTypeLibAm dataAm)
        {
            var data = _mapper.Map<AttributeTypeLibDm>(dataAm);
            data.Created = DateTime.Now.ToUniversalTime();
            data.CreatedBy = _contextAccessor?.GetName() ?? "Unknown";
            data.Id = data.Key.CreateMd5();
            var createdData = await _attributeTypeRepository.CreateAsync(data);
            await _attributeTypeRepository.SaveAsync();
            return _mapper.Map<AttributeTypeLibAm>(createdData.Entity);
        }

        public async Task CreateAttributeTypes(List<AttributeTypeLibAm> dataAm)
        {
            var dataList = _mapper.Map<List<AttributeTypeLibDm>>(dataAm);
            var existing = _attributeTypeRepository.GetAll().ToList();
            var notExisting = dataList.Where(x => existing.All(y => y.Id != x.Key.CreateMd5())).ToList();

            if (!notExisting.Any())
                return;

            foreach (var data in notExisting)
            {
                data.Created = DateTime.Now.ToUniversalTime();
                data.CreatedBy = _contextAccessor?.GetName() ?? "Unknown";
                data.Id = data.Key.CreateMd5();
                _attributeTypeRepository.Attach(data, EntityState.Added);
            }

            await _attributeTypeRepository.SaveAsync();

            foreach (var data in notExisting)
                _attributeTypeRepository.Detach(data);
        }
    }
}