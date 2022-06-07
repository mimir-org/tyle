using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Mimirorg.Common.Models;
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
        private readonly ApplicationSettings _applicationSettings;

        public PurposeService(IMapper mapper, IEfPurposeRepository purposeRepository, IOptions<ApplicationSettings> applicationSettings)
        {
            _mapper = mapper;
            _purposeRepository = purposeRepository;
            _applicationSettings = applicationSettings?.Value;
        }

        public Task<IEnumerable<PurposeLibCm>> GetPurposes()
        {
            var dataList = _purposeRepository.GetAll().Where(x => !x.Deleted).ToList()
                .OrderBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList();

            var dataAm = _mapper.Map<List<PurposeLibCm>>(dataList);
            return Task.FromResult(dataAm.AsEnumerable());
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