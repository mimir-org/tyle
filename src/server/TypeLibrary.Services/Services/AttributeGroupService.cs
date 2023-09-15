using AutoMapper;
using Lucene.Net.Util;
using Microsoft.Extensions.Logging;
using Mimirorg.Common.Exceptions;
using Mimirorg.Common.Extensions;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Services
{
    public class AttributeGroupService : IAttributeGroupService
    {
        private readonly IEfAttributeGroupRepository _attributeGroupRepository;
        private readonly IEfAttributeGroupAttributeRepository _attributeGroupAttributeRepository;
        private readonly IEfAttributeRepository _attributeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AttributeGroupService> _logger;


        public AttributeGroupService(IEfAttributeGroupRepository attributeGroupRepository, IEfAttributeGroupAttributeRepository attributeGroupAttributeRepository, IEfAttributeRepository attributeRepository, IMapper mapper, ILogger<AttributeGroupService> logger)
        {
            _attributeRepository = attributeRepository;
            _attributeGroupRepository = attributeGroupRepository;
            _attributeGroupAttributeRepository = attributeGroupAttributeRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <inheritdoc />
        public AttributeGroupLibCm GetSingleAttributeGroup(string id)
        {
            var dm = _attributeGroupRepository.GetSingleAttributeGroup(id);
            if (dm == null)
                throw new MimirorgNotFoundException($"Attribute with id {id} not found.");
            var item = _mapper.Map<AttributeGroupLibCm>(dm);

            return item;
        }

        /// <inheritdoc />
        public async Task<AttributeGroupLibCm> Create(AttributeGroupLibAm attributeGroupAm)
        {
            if (attributeGroupAm == null)
                throw new ArgumentNullException(nameof(attributeGroupAm));

            var attributeGroup = attributeGroupAm.ValidateObject();

            if (!attributeGroup.IsValid)
                throw new MimirorgBadRequestException("Attribute is not valid.", attributeGroup);

            var dm = _mapper.Map<AttributeGroupLibDm>(attributeGroupAm);

            var attributeGroupAttributes = new List<AttributeGroupAttributesLibDm>();

            if (attributeGroupAm.AttributeIds != null)
            {
                foreach (var attributeId in attributeGroupAm.AttributeIds)
                {
                    var attribute = _attributeRepository.Get(attributeId);

                    if (attribute == null)
                    {
                        _logger.LogError($"Could not add attribute with id {attributeId} to attribute group with id {dm.Id}, attribute not found.");
                    }
                    else
                    {
                        attributeGroupAttributes.Add(new AttributeGroupAttributesLibDm { AttributeGroupId = dm.Id, AttributeId = attribute.Id });
                    }
                }
            }

            dm.AttributeGroupAttributes = attributeGroupAttributes;

            var createdAttributeGroup = await _attributeGroupRepository.Create(dm);

            _attributeGroupRepository.ClearAllChangeTrackers();
            _logger.Log(LogLevel.Information, "Created attribute group", (createdAttributeGroup));

            return GetSingleAttributeGroup(createdAttributeGroup?.Id);
        }

        public async Task Delete(string id)
        {
            var dm = _attributeGroupRepository.GetSingleAttributeGroup(id) ?? throw new MimirorgNotFoundException($"Attribute with id {id} not found.");

            await _attributeGroupRepository.Delete(id);
            await _attributeGroupRepository.SaveAsync();
        }

        public IEnumerable<AttributeGroupLibCm> GetAttributeGroupList()
        {
            var dm = _attributeGroupRepository.GetAttributeGroupList().ToList();

            if (dm == null || !dm.Any())
                return new List<AttributeGroupLibCm>();

            return _mapper.Map<List<AttributeGroupLibCm>>(dm);
        }



        public async Task<AttributeGroupLibCm> Update(string id, AttributeGroupLibAm attributeGroupAm)
        {
            var attributeGroup = attributeGroupAm.ValidateObject();

            if (!attributeGroup.IsValid)
                throw new MimirorgBadRequestException("Attribute is not valid.", attributeGroup);

            var attributeGroupToUpdate = _attributeGroupRepository.GetSingleAttributeGroup(id);

            if (attributeGroupToUpdate == null)
                throw new Exception($"Could not find the Attribute group with id: {id}");

            attributeGroupToUpdate.Name = attributeGroupAm.Name;
            attributeGroupToUpdate.AttributeGroupAttributes ??= new List<AttributeGroupAttributesLibDm>();
            attributeGroupToUpdate.Attributes ??= new List<AttributeLibDm>();

            var itemsAlreadyInDb = attributeGroupToUpdate.AttributeGroupAttributes.Select(x => x.AttributeId).ToList();

            foreach (var item in itemsAlreadyInDb.Except(attributeGroupAm.AttributeIds.ToList()).ToList())
            {
                var attributeGroupAttributeId = attributeGroupToUpdate.AttributeGroupAttributes.Where(x => x.AttributeId.Equals(item)).FirstOrDefault();
                await _attributeGroupAttributeRepository.Delete(attributeGroupAttributeId.Id);
            }

            foreach (var attributeGroupAttributeItem in attributeGroupAm.AttributeIds.ToList().Except(itemsAlreadyInDb).ToList())
            {
                if (!attributeGroupToUpdate.AttributeGroupAttributes.Select(x => x.AttributeId).Contains(attributeGroupAttributeItem))
                {
                    var attributeExist = _attributeRepository.FindBy(x => x.Id.Equals(attributeGroupAttributeItem)).FirstOrDefault();
                    if (attributeExist != null)
                    {
                        await _attributeGroupAttributeRepository.CreateAsync(new AttributeGroupAttributesLibDm { AttributeId = attributeExist.Id, AttributeGroupId = attributeGroupToUpdate.Id });
                    }
                }
            }

            await _attributeGroupRepository.SaveAsync();
            await _attributeGroupAttributeRepository.SaveAsync();

            return GetSingleAttributeGroup(attributeGroupToUpdate.Id);
        }
    }
}