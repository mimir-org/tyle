using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using Mimirorg.Common.Models;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Services
{
    public class RdsService : IRdsService
    {
        private readonly IMapper _mapper;
        private readonly IRdsRepository _rdsRepository;
        private readonly ApplicationSettings _applicationSettings;

        public RdsService(IMapper mapper, IRdsRepository rdsRepository, IOptions<ApplicationSettings> applicationSettings)
        {
            _mapper = mapper;
            _rdsRepository = rdsRepository;
            _applicationSettings = applicationSettings?.Value;
        }

        public IEnumerable<RdsLibCm> Get()
        {
            var allRds = _rdsRepository.Get().Where(x => x.State != State.Deleted).ToList()
                .OrderBy(x => x.Id.Length).ThenBy(x => x.Id, StringComparer.InvariantCultureIgnoreCase).ToList();

            return _mapper.Map<List<RdsLibCm>>(allRds);
        }

        public async Task Create(List<RdsLibAm> createRds, bool createdBySystem = false)
        {
            var dataList = _mapper.Map<List<RdsLibDm>>(createRds);
            var existing = _rdsRepository.Get().ToList();
            var notExisting = dataList.Where(x => existing.All(y => y.Id != x.Id)).ToList();

            if (!notExisting.Any())
                return;

            foreach (var data in notExisting)
                data.CreatedBy = createdBySystem ? _applicationSettings.System : data.CreatedBy;

            await _rdsRepository.Create(notExisting, createdBySystem ? State.ApprovedGlobal : State.Draft);
            _rdsRepository.ClearAllChangeTrackers();
        }
    }
}