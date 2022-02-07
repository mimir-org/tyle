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
    public class FormatService : IFormatService
    {
        private readonly IMapper _mapper;
        private readonly IFormatRepository _formatRepository;
        private readonly IHttpContextAccessor _contextAccessor;

        public FormatService(IMapper mapper, IFormatRepository formatRepository, IHttpContextAccessor contextAccessor)
        {
            _mapper = mapper;
            _formatRepository = formatRepository;
            _contextAccessor = contextAccessor;
        }

        public Task<IEnumerable<FormatLibAm>> GetFormats()
        {
            var dataList = _formatRepository.GetAll();
            var dataAm = _mapper.Map<List<FormatLibAm>>(dataList);
            return Task.FromResult<IEnumerable<FormatLibAm>>(dataAm);
        }

        public async Task<FormatLibAm> UpdateFormat(FormatLibAm dataAm)
        {
            var data = _mapper.Map<FormatLibDm>(dataAm);
            data.Updated = DateTime.Now.ToUniversalTime();
            data.UpdatedBy = _contextAccessor?.GetName() ?? "Unknown";
            _formatRepository.Update(data);
            await _formatRepository.SaveAsync();
            return _mapper.Map<FormatLibAm>(data);
        }

        public async Task<FormatLibAm> CreateFormat(FormatLibAm dataAm)
        {
            var data = _mapper.Map<FormatLibDm>(dataAm);
            data.Created = DateTime.Now.ToUniversalTime();
            data.CreatedBy = _contextAccessor?.GetName() ?? "Unknown";
            data.Id = data.Key.CreateMd5();
            var createdData = await _formatRepository.CreateAsync(data);
            await _formatRepository.SaveAsync();
            return _mapper.Map<FormatLibAm>(createdData.Entity);
        }

        public async Task CreateFormats(List<FormatLibAm> dataAm)
        {
            var dataList = _mapper.Map<List<FormatLibDm>>(dataAm);
            var existing = _formatRepository.GetAll().ToList();
            var notExisting = dataList.Where(x => existing.All(y => y.Id != x.Key.CreateMd5())).ToList();

            if (!notExisting.Any())
                return;

            foreach (var data in notExisting)
            {
                data.Created = DateTime.Now.ToUniversalTime();
                data.CreatedBy = _contextAccessor?.GetName() ?? "Unknown";
                data.Id = data.Key.CreateMd5();
                _formatRepository.Attach(data, EntityState.Added);
            }

            await _formatRepository.SaveAsync();

            foreach (var data in notExisting)
                _formatRepository.Detach(data);
        }
    }
}