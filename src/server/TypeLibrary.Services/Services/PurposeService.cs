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
    public class PurposeService : IPurposeService
    {
        private readonly IMapper _mapper;
        private readonly IPurposeRepository _purposeRepository;
        private readonly IHttpContextAccessor _contextAccessor;

        public PurposeService(IMapper mapper, IPurposeRepository purposeRepository, IHttpContextAccessor contextAccessor)
        {
            _mapper = mapper;
            _purposeRepository = purposeRepository;
            _contextAccessor = contextAccessor;
        }

        public Task<IEnumerable<PurposeAm>> GetPurposes()
        {
            var dataList = _purposeRepository.GetAll();
            var dataAm = _mapper.Map<List<PurposeAm>>(dataList);
            return Task.FromResult<IEnumerable<PurposeAm>>(dataAm);
        }

        public async Task<PurposeAm> UpdatePurpose(PurposeAm dataAm)
        {
            var data = _mapper.Map<PurposeDm>(dataAm);
            data.Updated = DateTime.Now.ToUniversalTime();
            data.UpdatedBy = _contextAccessor?.GetName() ?? "Unknown";
            _purposeRepository.Update(data);
            await _purposeRepository.SaveAsync();
            return _mapper.Map<PurposeAm>(data);
        }

        public async Task<PurposeAm> CreatePurpose(PurposeAm dataAm)
        {
            var data = _mapper.Map<PurposeDm>(dataAm);
            data.Created = DateTime.Now.ToUniversalTime();
            data.CreatedBy = _contextAccessor?.GetName() ?? "Unknown";
            data.Id = data.Key.CreateMd5();
            var createdData = await _purposeRepository.CreateAsync(data);
            await _purposeRepository.SaveAsync();
            return _mapper.Map<PurposeAm>(createdData.Entity);
        }

        public async Task CreatePurposes(List<PurposeAm> dataAm)
        {
            var dataList = _mapper.Map<List<PurposeDm>>(dataAm);
            var existing = _purposeRepository.GetAll().ToList();
            var notExisting = dataList.Where(x => existing.All(y => y.Id != x.Key.CreateMd5())).ToList();

            if (!notExisting.Any())
                return;

            foreach (var data in notExisting)
            {
                data.Created = DateTime.Now.ToUniversalTime();
                data.CreatedBy = _contextAccessor?.GetName() ?? "Unknown";
                data.Id = data.Key.CreateMd5();
                _purposeRepository.Attach(data, EntityState.Added);
            }

            await _purposeRepository.SaveAsync();

            foreach (var data in notExisting)
                _purposeRepository.Detach(data);
        }
    }
}