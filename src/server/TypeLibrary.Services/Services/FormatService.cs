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

        public Task<IEnumerable<FormatAm>> GetFormats()
        {
            var dataList = _formatRepository.GetAll();
            var dataAm = _mapper.Map<List<FormatAm>>(dataList);
            return Task.FromResult<IEnumerable<FormatAm>>(dataAm);
        }

        public async Task<FormatAm> UpdateFormat(FormatAm dataAm)
        {
            var data = _mapper.Map<FormatDm>(dataAm);
            data.Updated = DateTime.Now.ToUniversalTime();
            data.UpdatedBy = _contextAccessor?.GetName() ?? "Unknown";
            _formatRepository.Update(data);
            await _formatRepository.SaveAsync();
            return _mapper.Map<FormatAm>(data);
        }

        public async Task<FormatAm> CreateFormat(FormatAm dataAm)
        {
            var data = _mapper.Map<FormatDm>(dataAm);
            data.Created = DateTime.Now.ToUniversalTime();
            data.CreatedBy = _contextAccessor?.GetName() ?? "Unknown";
            data.Id = data.Key.CreateMd5();
            var createdData = await _formatRepository.CreateAsync(data);
            await _formatRepository.SaveAsync();
            return _mapper.Map<FormatAm>(createdData.Entity);
        }

        public async Task CreateFormats(List<FormatAm> dataAm)
        {
            var dataList = _mapper.Map<List<FormatDm>>(dataAm);
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