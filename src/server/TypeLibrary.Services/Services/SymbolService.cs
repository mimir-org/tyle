using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Mimirorg.Common.Exceptions;
using TypeLibrary.Data.Contracts;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Models;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Services
{
    public class SymbolService : ISymbolService
    {
        private readonly IMapper _mapper;
        private readonly ISymbolRepository _symbolRepository;

        public SymbolService(IMapper mapper, ISymbolRepository symbolRepository)
        {
            _mapper = mapper;
            _symbolRepository = symbolRepository;
        }

        /// <summary>
        /// Create symbol data
        /// </summary>
        /// <param name="symbolLibAm"></param>
        /// <param name="saveData"></param>
        /// <returns></returns>
        public async Task<SymbolLibCm> CreateSymbol(SymbolLibAm symbolLibAm, bool saveData = true)
        {
            var symbolExist = await _symbolRepository.GetAsync(symbolLibAm.Id);

            if (symbolExist != null)
                throw new MimirorgDuplicateException($"There is already a symbol with name: {symbolLibAm.Name}");

            var dm = _mapper.Map<SymbolLibDm>(symbolLibAm);
            await _symbolRepository.CreateAsync(dm);

            if (saveData)
                await _symbolRepository.SaveAsync();

            return _mapper.Map<SymbolLibCm>(dm);
        }

        /// <summary>
        /// Create symbol data from list
        /// </summary>
        /// <param name="symbolLibAmList"></param>
        /// <returns></returns>
        public async Task<IEnumerable<SymbolLibCm>> CreateSymbol(IEnumerable<SymbolLibAm> symbolLibAmList)
        {
            var symbols = new List<SymbolLibDm>();
            var existingSymbols = _symbolRepository.GetAll().ToList();

            foreach (var symbol in symbolLibAmList)
            {
                var symbolExist = existingSymbols.FirstOrDefault(x => x.Id == symbol.Id);
                if (symbolExist != null)
                    continue;

                var dm = _mapper.Map<SymbolLibDm>(symbol);
                await _symbolRepository.CreateAsync(dm);
                symbols.Add(dm);
            }

            await _symbolRepository.SaveAsync();
            return _mapper.Map<List<SymbolLibCm>>(symbols);
        }

        /// <summary>
        /// Update a symbol
        /// </summary>
        /// <param name="id"></param>
        /// <param name="symbolLibAm"></param>
        /// <returns></returns>
        public async Task<SymbolLibCm> UpdateSymbol(string id, SymbolLibAm symbolLibAm)
        {
            var dm = await _symbolRepository.GetAsync(id);
            if (dm == null)
                throw new MimirorgNotFoundException($"There is no symbol data with id: {id}");

            _symbolRepository.Update(dm);
            await _symbolRepository.SaveAsync();
            return _mapper.Map<SymbolLibCm>(dm);
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