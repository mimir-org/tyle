using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using Mimirorg.Common.Models;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Services
{
    public class SymbolService : ISymbolService
    {
        private readonly IMapper _mapper;
        private readonly IEfSymbolRepository _symbolRepository;
        private readonly ApplicationSettings _applicationSettings;

        public SymbolService(IMapper mapper, IEfSymbolRepository symbolRepository, IOptions<ApplicationSettings> applicationSettings)
        {
            _mapper = mapper;
            _symbolRepository = symbolRepository;
            _applicationSettings = applicationSettings?.Value;
        }

        /// <summary>
        /// Create symbol data from list
        /// </summary>
        /// <param name="symbolLibAmList"></param>
        /// <param name="createdBySystem"></param>
        /// <returns></returns>
        public async Task<IEnumerable<SymbolLibCm>> CreateSymbol(IEnumerable<SymbolLibAm> symbolLibAmList, bool createdBySystem = false)
        {
            var symbols = new List<SymbolLibDm>();
            var existingSymbols = _symbolRepository.GetAll().ToList();

            foreach (var symbol in symbolLibAmList)
            {
                var symbolExist = existingSymbols.FirstOrDefault(x => x.Id == symbol.Id);
                if (symbolExist != null)
                    continue;

                var dm = _mapper.Map<SymbolLibDm>(symbol);
                dm.CreatedBy = createdBySystem ? _applicationSettings.System : dm.CreatedBy;

                await _symbolRepository.CreateAsync(dm);
                symbols.Add(dm);
            }

            await _symbolRepository.SaveAsync();
            return _mapper.Map<List<SymbolLibCm>>(symbols);
        }

        /// <summary>
        /// Get all symbols
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SymbolLibCm> GetSymbol()
        {
            var symbolLibDms = _symbolRepository.GetAll().Where(x => !x.Deleted).ToList()
                .OrderBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList();

            return _mapper.Map<List<SymbolLibCm>>(symbolLibDms);
        }
    }
}