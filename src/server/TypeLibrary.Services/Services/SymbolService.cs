using AutoMapper;
using Mimirorg.Common.Enums;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Services;

public class SymbolService : ISymbolService
{
    private readonly IMapper _mapper;
    private readonly ISymbolRepository _symbolRepository;
    private readonly ILogService _logService;

    public SymbolService(IMapper mapper, ISymbolRepository symbolRepository, ILogService logService)
    {
        _mapper = mapper;
        _symbolRepository = symbolRepository;
        _logService = logService;
    }

    public IEnumerable<SymbolLibCm> Get()
    {
        var symbolLibDms = _symbolRepository.Get().Where(x => x.State != State.Deleted).ToList()
            .OrderBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList();

        return _mapper.Map<List<SymbolLibCm>>(symbolLibDms);
    }

    public async Task Create(IEnumerable<SymbolLibAm> symbolLibAmList, string createdBy = null)
    {
        var dataList = _mapper.Map<List<SymbolLibDm>>(symbolLibAmList);
        var existing = _symbolRepository.Get().ToList();
        var notExisting = dataList.Where(x => existing.All(y => y.Name != x.Name)).ToList();

        if (!notExisting.Any())
            return;

        foreach (var data in notExisting)
        {
            data.CreatedBy = string.IsNullOrEmpty(createdBy) ? data.CreatedBy : createdBy;
        }

        await _symbolRepository.Create(notExisting, string.IsNullOrEmpty(createdBy) ? State.Draft : State.Approved);

        await _logService.CreateLogs(
            notExisting,
            LogType.Create,
            string.IsNullOrEmpty(createdBy) ? State.Draft.ToString() : State.Approved.ToString(), notExisting[0]?.CreatedBy);

        _symbolRepository.ClearAllChangeTrackers();
    }
}