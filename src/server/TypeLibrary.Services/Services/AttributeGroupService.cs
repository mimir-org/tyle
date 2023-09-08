using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Common.Enums;
using Mimirorg.Common.Exceptions;
using Mimirorg.Common.Extensions;
using Mimirorg.TypeLibrary.Constants;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypeLibrary.Data.Constants;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;
using TypeLibrary.Data.Repositories.Ef;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Services
{
    public class AttributeGroupService : IAttributeGroupService
    {
        private readonly IEfAttributeGroupRepository _attributeGroupRepository;
        private readonly ILogService _logService;
        private readonly IEfAttributeRepository _attributeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AttributeGroupService> _logger;


        public AttributeGroupService(IEfAttributeGroupRepository attributeGroupRepository, IEfAttributeRepository attributeRepository, ILogService logService, IMapper mapper, ILogger<AttributeGroupService> logger)
        {
            _logService = logService;
            _attributeRepository = attributeRepository;
            _attributeGroupRepository = attributeGroupRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <inheritdoc />
        public AttributeGroupLibCm GetSingleAttributeGroup(string id)
        {
            var dm = _attributeGroupRepository.GetSingleAttributeGroup(id);
            if (dm == null)
                throw new MimirorgNotFoundException($"Attribute with id {id} not found.");
            return _mapper.Map<AttributeGroupLibCm>(dm);

        }

        /// <inheritdoc />
        public IEnumerable<AttributeGroupLibCm> GetAttributeGroupList()
        {
            var dm = _attributeGroupRepository.GetAttributeGroupList().ToList();

            if (dm == null || !dm.Any())
                return new List<AttributeGroupLibCm>();

            return _mapper.Map<List<AttributeGroupLibCm>>(dm);

        }

        /// <inheritdoc />
        public async Task<AttributeGroupLibCm> Create(AttributeGroupLibAm attributeGroupAm)
        {
            if (attributeGroupAm == null)
                throw new ArgumentNullException(nameof(attributeGroupAm));

            var validation = attributeGroupAm.ValidateObject();

            if (!validation.IsValid)
                throw new MimirorgBadRequestException("Attribute is not valid.", validation);

            var dm = _mapper.Map<AttributeGroupLibDm>(attributeGroupAm);


            dm.Attribute = new List<AttributeGroupAttributesLibDm>();

            if (attributeGroupAm.Attributes != null)
            {
                foreach (var attributeId in attributeGroupAm.Attributes)
                {
                    var attribute = _attributeRepository.Get(attributeId);

                    if (attribute == null)
                    {
                        _logger.LogError($"Could not add attribute with id {attributeId} to attribute group with id {dm.Id}, attribute not found.");
                    }
                    else
                    {
                        dm.Attribute.Add(new AttributeGroupAttributesLibDm { AttributeGroupId = dm.Id, AttributeId = attribute.Id });
                    }
                }
            }



            var createdAttributeGroup = await _attributeGroupRepository.Create(dm);
            _attributeGroupRepository.ClearAllChangeTrackers();
            _logger.Log(LogLevel.Information, "Created attribute group", (createdAttributeGroup));

            return _mapper.Map<AttributeGroupLibCm>(createdAttributeGroup);
        }

        public async Task Delete(string id)
        {
            var dm = _attributeGroupRepository.GetSingleAttributeGroup(id) ?? throw new MimirorgNotFoundException($"Attribute with id {id} not found.");

            await _attributeGroupRepository.Delete(id);
            await _attributeGroupRepository.SaveAsync();
        }

        IEnumerable<AttributeGroupLibCm> IAttributeGroupService.GetAttributeGroupList()
        {
            var dm = _attributeGroupRepository.GetAll().ToList();

            if (dm == null || !dm.Any())
                return new List<AttributeGroupLibCm>();

            return _mapper.Map<List<AttributeGroupLibCm>>(dm);
        }



        public async Task<AttributeGroupLibCm> Update(string id, AttributeGroupLibAm attributeAm)
        {
            var itemFromDb = _attributeGroupRepository.GetSingleAttributeGroup(id);
            if (itemFromDb == null)
                throw new Exception($"Could not find the Attribute group with id: {id}");

            await Delete(id);
            var itemCreated = await Create(attributeAm);

            return itemCreated;
        }
    }

}