using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Mimirorg.Common.Extensions;
using Mimirorg.TypeLibrary.Enums;
using TypeLibrary.Data.Contracts;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Models;
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

        public Task<IEnumerable<AttributeSourceLibCm>> GetAttributeSources()
        {
            var notSet = _sourceRepository.FindBy(x => !x.Deleted && x.Name == Aspect.NotSet.ToString())?.First();
            var dataSet = _sourceRepository.GetAll().Where(x => !x.Deleted && x.Name != Aspect.NotSet.ToString()).ToList();

            var dataDmList = new List<AttributeSourceLibDm> { notSet };
            dataDmList.AddRange(dataSet.OrderBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList());

            var dataCm = _mapper.Map<List<AttributeSourceLibCm>>(dataDmList);
            return Task.FromResult(dataCm.AsEnumerable());
        }

        public async Task<AttributeSourceLibCm> UpdateAttributeSource(AttributeSourceLibAm dataAm, int id)
        {
            var data = _mapper.Map<AttributeSourceLibDm>(dataAm);
            data.Id = id;
            data.Updated = DateTime.Now.ToUniversalTime();
            data.UpdatedBy = _contextAccessor?.GetName() ?? "Unknown";
            _sourceRepository.Update(data);
            await _sourceRepository.SaveAsync();
            return _mapper.Map<AttributeSourceLibCm>(data);
        }

        public async Task<AttributeSourceLibCm> CreateAttributeSource(AttributeSourceLibAm dataAm)
        {
            var data = _mapper.Map<AttributeSourceLibDm>(dataAm);
            data.Created = DateTime.Now.ToUniversalTime();
            data.CreatedBy = _contextAccessor?.GetName() ?? "Unknown";
            var createdData = await _sourceRepository.CreateAsync(data);
            await _sourceRepository.SaveAsync();
            return _mapper.Map<AttributeSourceLibCm>(createdData.Entity);
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