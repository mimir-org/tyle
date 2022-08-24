using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Services
{
    public class UnitService : IUnitService
    {
        private readonly IMapper _mapper;
        private readonly IUnitRepository _unitRepository;

        public UnitService(IMapper mapper, IUnitRepository unitRepository)
        {
            _mapper = mapper;
            _unitRepository = unitRepository;
        }

        public Task<IEnumerable<UnitLibCm>> Get()
        {
            var dataList = _unitRepository.GetUnits().ToList()
                .OrderBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList();

            var dataAm = _mapper.Map<List<UnitLibCm>>(dataList);
            return Task.FromResult(dataAm.AsEnumerable());
        }

        public Task<UnitLibCm> Get(string id)
        {
            var unit = _unitRepository.GetUnits().FirstOrDefault(x => x.Id == id);
            if (unit == null)
                return null;

            var data = _mapper.Map<UnitLibCm>(unit);
            return Task.FromResult(data);
        }
    }
}