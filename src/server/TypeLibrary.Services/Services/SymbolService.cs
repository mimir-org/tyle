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
    public class SymbolService : ISymbolService
    {
        private readonly IMapper _mapper;
        private readonly ISymbolRepository _symbolRepository;
        private readonly ApplicationSettings _applicationSettings;

        public SymbolService(IMapper mapper, ISymbolRepository symbolRepository, IOptions<ApplicationSettings> applicationSettings)
        {
            _mapper = mapper;
            _symbolRepository = symbolRepository;
            _applicationSettings = applicationSettings?.Value;
        }

        public IEnumerable<SymbolLibCm> Get()
        {
            var symbolLibDms = _symbolRepository.Get().Where(x => x.State != State.Deleted).ToList()
                .OrderBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList();

            return _mapper.Map<List<SymbolLibCm>>(symbolLibDms);
        }

        public async Task Create(IEnumerable<SymbolLibAm> symbolLibAmList, bool createdBySystem = false)
        {
            var dataList = _mapper.Map<List<SymbolLibDm>>(symbolLibAmList);
            var existing = _symbolRepository.Get().ToList();
            var notExisting = dataList.Where(x => existing.All(y => y.Id != x.Id)).ToList();

            if (!notExisting.Any())
                return;

            foreach (var data in notExisting)
                data.CreatedBy = createdBySystem ? _applicationSettings.System : data.CreatedBy;

            await _symbolRepository.Create(notExisting, createdBySystem ? State.ApprovedGlobal : State.Draft);
            _symbolRepository.ClearAllChangeTrackers();
        }
    }
}