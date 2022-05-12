using System.Collections.Generic;
using System.Linq;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Factories
{
    public class UnitFactory : IUnitFactory
    {
        public ICollection<UnitLibDm> AllUnits { get; private set; }
        private readonly IEfUnitRepository _unitRepository;

        public UnitFactory(IEfUnitRepository unitRepository)
        {
            _unitRepository = unitRepository;
        }

        public UnitLibDm Get(string id)
        {
            if (AllUnits == null || !AllUnits.Any())
                AllUnits = _unitRepository.GetAll().ToList();

            return AllUnits?.FirstOrDefault(x => x.Id == id);
        }
    }
}
