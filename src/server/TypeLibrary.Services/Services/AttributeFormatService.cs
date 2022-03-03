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
    public class AttributeFormatService : IAttributeFormatService
    {
        private readonly IMapper _mapper;
        private readonly IFormatRepository _formatRepository;
        private readonly IHttpContextAccessor _contextAccessor;

        public AttributeFormatService(IMapper mapper, IFormatRepository formatRepository, IHttpContextAccessor contextAccessor)
        {
            _mapper = mapper;
            _formatRepository = formatRepository;
            _contextAccessor = contextAccessor;
        }

        public Task<IEnumerable<AttributeFormatLibCm>> GetAttributeFormats()
        {
            var dataList = _formatRepository.GetAll();
            var dataAm = _mapper.Map<List<AttributeFormatLibCm>>(dataList);
            return Task.FromResult(dataAm.AsEnumerable());
        }

        public async Task<AttributeFormatLibCm> UpdateAttributeFormat(AttributeFormatLibAm dataAm, string id)
        {
            var data = _mapper.Map<AttributeFormatLibDm>(dataAm);
            data.Id = int.Parse(id);
            data.Updated = DateTime.Now.ToUniversalTime();
            data.UpdatedBy = _contextAccessor?.GetName() ?? "Unknown";
            _formatRepository.Update(data);
            await _formatRepository.SaveAsync();
            return _mapper.Map<AttributeFormatLibCm>(data);
        }

        public async Task<AttributeFormatLibCm> CreateAttributeFormat(AttributeFormatLibAm dataAm)
        {
            var data = _mapper.Map<AttributeFormatLibDm>(dataAm);
            data.Created = DateTime.Now.ToUniversalTime();
            data.CreatedBy = _contextAccessor?.GetName() ?? "Unknown";
            var createdData = await _formatRepository.CreateAsync(data);
            await _formatRepository.SaveAsync();
            return _mapper.Map<AttributeFormatLibCm>(createdData.Entity);
        }

        public async Task CreateAttributeFormats(List<AttributeFormatLibAm> dataAm)
        {
            var dataList = _mapper.Map<List<AttributeFormatLibDm>>(dataAm);
            var existing = _formatRepository.GetAll().ToList();
            var notExisting = dataList.Where(x => existing.All(y => y.Name != x.Name)).ToList();

            if (!notExisting.Any())
                return;

            foreach (var data in notExisting)
            {
                data.Created = DateTime.Now.ToUniversalTime();
                data.CreatedBy = _contextAccessor?.GetName() ?? "Unknown";
                _formatRepository.Attach(data, EntityState.Added);
            }

            await _formatRepository.SaveAsync();

            foreach (var data in notExisting)
                _formatRepository.Detach(data);
        }
    }
}