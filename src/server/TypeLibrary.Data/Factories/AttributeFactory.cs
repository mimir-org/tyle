using System.Collections.Generic;
using System.Linq;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Factories
{
    public class AttributeFactory : IAttributeFactory
    {
        public ICollection<AttributeLibDm> AllAttributes { get; private set; }
        private readonly IEfAttributeRepository _attributeRepository;

        public AttributeFactory(IEfAttributeRepository attributeRepository)
        {
            _attributeRepository = attributeRepository;
        }

        public AttributeLibDm Get(string id)
        {
            if (AllAttributes == null || !AllAttributes.Any())
                AllAttributes = _attributeRepository.GetAll().ToList();

            return AllAttributes?.FirstOrDefault(x => x.Id == id);
        }
    }
}
