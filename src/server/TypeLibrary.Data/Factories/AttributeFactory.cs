using System.Collections.Generic;
using System.Linq;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Contracts.Factories;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Factories
{
    public class AttributeFactory : IAttributeFactory
    {
        public ICollection<AttributeLibDm> AllAttributes { get; private set; }
        private readonly IAttributeRepository _attributeRepository;

        public AttributeFactory(IAttributeRepository attributeRepository)
        {
            _attributeRepository = attributeRepository;
        }

        public AttributeLibDm Get(string id)
        {
            if (AllAttributes == null || !AllAttributes.Any())
                AllAttributes = _attributeRepository.Get().ToList();

            return AllAttributes?.FirstOrDefault(x => x.Id == id);
        }
    }
}