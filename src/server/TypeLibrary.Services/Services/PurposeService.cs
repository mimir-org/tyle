using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Mimirorg.Common.Exceptions;
using Mimirorg.Common.Extensions;
using Mimirorg.Common.Models;
using TypeLibrary.Data.Contracts;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Services
{
    public class PurposeService : IPurposeService
    {
        private readonly IMapper _mapper;
        private readonly IEfPurposeRepository _purposeRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ApplicationSettings _applicationSettings;

        public PurposeService(IMapper mapper, IEfPurposeRepository purposeRepository, IHttpContextAccessor contextAccessor, IOptions<ApplicationSettings> applicationSettings)
        {
            _mapper = mapper;
            _purposeRepository = purposeRepository;
            _contextAccessor = contextAccessor;
            _applicationSettings = applicationSettings?.Value;
        }

        public Task<IEnumerable<PurposeLibCm>> GetPurposes()
        {
            var dataList = _purposeRepository.GetAll().Where(x => !x.Deleted).ToList()
                .OrderBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList();

            var dataAm = _mapper.Map<List<PurposeLibCm>>(dataList);
            return Task.FromResult(dataAm.AsEnumerable());
        }

        public async Task<PurposeLibCm> UpdatePurpose(PurposeLibAm dataAm, string id)
        {
            if (string.IsNullOrWhiteSpace(id) || dataAm == null)
                throw new MimirorgBadRequestException("The data object or id can not be null.");

            var existingDm = await _purposeRepository.GetAsync(id);

            if (existingDm?.Id == null)
                throw new MimirorgBadRequestException("Object not found.");

            if (existingDm.CreatedBy == _applicationSettings.System)
                throw new MimirorgBadRequestException($"The object with id {id} is created by the system and can not be updated.");

            //TODO: The code below must be rewritten. What do we allow to be updated?
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
            var createdData = await _purposeRepository.CreateAsync(data);
            await _purposeRepository.SaveAsync();
            return _mapper.Map<PurposeLibCm>(createdData.Entity);
        }

        public async Task CreatePurposes(List<PurposeLibAm> dataAm, bool createdBySystem = false)
        {
            var dataList = _mapper.Map<List<PurposeLibDm>>(dataAm);
            var existing = _purposeRepository.GetAll().ToList();
            var notExisting = dataList.Where(x => existing.All(y => y.Id != x.Id)).ToList();

            if (!notExisting.Any())
                return;

            foreach (var data in notExisting)
            {
                data.CreatedBy = createdBySystem ? _applicationSettings.System : data.CreatedBy;
                _purposeRepository.Attach(data, EntityState.Added);
            }

            await _purposeRepository.SaveAsync();

            foreach (var data in notExisting)
                _purposeRepository.Detach(data);
        }
    }
}