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

        public Task<IEnumerable<PurposeLibCm>> GetPurposes()
        {
            var dataList = _purposeRepository.GetAll().ToList().OrderBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList();
            var dataAm = _mapper.Map<List<PurposeLibCm>>(dataList);
            return Task.FromResult(dataAm.AsEnumerable());
        }

        public async Task<PurposeLibCm> UpdatePurpose(PurposeLibAm dataAm, string id)
        {
            var data = _mapper.Map<PurposeLibDm>(dataAm);
            data.Id = id;
            data.Updated = DateTime.Now.ToUniversalTime();
            data.UpdatedBy = _contextAccessor?.GetName() ?? "Unknown";
            _purposeRepository.Update(data);
            await _purposeRepository.SaveAsync();
            return _mapper.Map<PurposeLibCm>(data);
        }

        public async Task<PurposeLibCm> CreatePurpose(PurposeLibAm dataAm)
        {
            var data = _mapper.Map<PurposeLibDm>(dataAm);
            data.Created = DateTime.Now.ToUniversalTime();
            data.CreatedBy = _contextAccessor?.GetName() ?? dataAm.CreatedBy ?? "Unknown";
            var createdData = await _purposeRepository.CreateAsync(data);
            await _purposeRepository.SaveAsync();
            return _mapper.Map<PurposeLibCm>(createdData.Entity);
        }

        public async Task CreatePurposes(List<PurposeLibAm> dataAm)
        {
            var dataList = _mapper.Map<List<PurposeLibDm>>(dataAm);
            var existing = _purposeRepository.GetAll().ToList();
            var notExisting = dataList.Where(x => existing.All(y => y.Id != x.Id)).ToList();

            if (!notExisting.Any())
                return;

            foreach (var data in notExisting)
            {
                data.Created = DateTime.Now.ToUniversalTime();
                data.CreatedBy = _contextAccessor?.GetName() ?? data.CreatedBy ?? "Unknown";
                _purposeRepository.Attach(data, EntityState.Added);
            }

            await _purposeRepository.SaveAsync();

            foreach (var data in notExisting)
                _purposeRepository.Detach(data);
        }
    }
}