using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Common.Enums;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Services;

public class UnitService : IUnitService
{
    private readonly IMapper _mapper;
    private readonly IUnitRepository _unitRepository;
    private readonly ITimedHookService _hookService;

    public UnitService(IMapper mapper, IUnitRepository unitRepository, ITimedHookService hookService)
    {
        _mapper = mapper;
        _unitRepository = unitRepository;
        _hookService = hookService;
    }

    public IEnumerable<UnitLibCm> Get()
    {
        var dataList = _unitRepository.Get();

        var dataAm = _mapper.Map<List<UnitLibCm>>(dataList);
        return dataAm.AsEnumerable();
    }

    public UnitLibCm Get(int id)
    {
        var unit = (_unitRepository.Get()).FirstOrDefault(x => x.Id == id);
        if (unit == null)
            return null;

        var data = _mapper.Map<UnitLibCm>(unit);
        return data;
    }

    public async Task<UnitLibCm> Create(UnitLibAm unitAm)
    {
        if (unitAm == null)
            throw new ArgumentNullException(nameof(unitAm));

        var dm = _mapper.Map<UnitLibDm>(unitAm);

        dm.State = State.Draft;

        var createdUnit = await _unitRepository.Create(dm);
        _unitRepository.ClearAllChangeTrackers();
        _hookService.HookQueue.Enqueue(CacheKey.Unit);

        return _mapper.Map<UnitLibCm>(createdUnit);
    }
}