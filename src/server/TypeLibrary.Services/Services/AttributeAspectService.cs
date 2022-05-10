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
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Models;
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

        public Task<IEnumerable<AttributeAspectLibCm>> GetAttributeAspects()
        {
            var allAttributes = _attributeAspectRepository.GetAll().ToList();
            var attributes = allAttributes.Where(x => x.ParentId != null).ToList();
            var topParents = allAttributes.Where(x => x.ParentId == null).OrderBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList();
            
            var sortedAttributes = attributes.OrderBy(x => topParents
                .FirstOrDefault(y => y.Id == x.ParentId)?.Name, StringComparer.InvariantCultureIgnoreCase)
                .ThenBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList();
            
            sortedAttributes.AddRange(topParents);
            var dataCm = _mapper.Map<List<AttributeAspectLibCm>>(sortedAttributes);
            return Task.FromResult(dataCm.AsEnumerable());
        }

        public async Task<AttributeAspectLibCm> UpdateAttributeAspect(AttributeAspectLibAm dataAm, string id)
        {
            var data = _mapper.Map<AttributeAspectLibDm>(dataAm);
            data.Id = id;
            data.Updated = DateTime.Now.ToUniversalTime();
            data.UpdatedBy = _contextAccessor?.GetName() ?? "Unknown";
            _attributeAspectRepository.Update(data);
            await _attributeAspectRepository.SaveAsync();
            return _mapper.Map<AttributeAspectLibCm>(data);
        }

        public async Task<AttributeAspectLibCm> CreateAttributeAspect(AttributeAspectLibAm dataAm)
        {
            var data = _mapper.Map<AttributeAspectLibDm>(dataAm);
            data.Created = DateTime.Now.ToUniversalTime();
            data.CreatedBy = _contextAccessor?.GetName() ?? "Unknown";
            var createdData = await _attributeAspectRepository.CreateAsync(data);
            await _attributeAspectRepository.SaveAsync();
            return _mapper.Map<AttributeAspectLibCm>(createdData.Entity);
        }

        public async Task CreateAttributeAspects(List<AttributeAspectLibAm> dataAm)
        {
            var dataList = _mapper.Map<List<AttributeAspectLibDm>>(dataAm);
            var existing = _attributeAspectRepository.GetAll().ToList();
            var notExisting = dataList.Where(x => existing.All(y => y.Id != x.Id)).ToList();

            if (!notExisting.Any())
                return;

            foreach (var data in notExisting)
            {
                data.Created = DateTime.Now.ToUniversalTime();
                data.CreatedBy = _contextAccessor?.GetName() ?? "Unknown";
                _attributeAspectRepository.Attach(data, EntityState.Added);
            }

            await _attributeAspectRepository.SaveAsync();

            foreach (var data in notExisting)
                _attributeAspectRepository.Detach(data);
        }
    }
}