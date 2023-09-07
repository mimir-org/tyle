using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Common.Enums;
using Mimirorg.Common.Exceptions;
using Mimirorg.Common.Extensions;
using Mimirorg.TypeLibrary.Enums;
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
        public async Task<AttributeGroupLibCm> Create(AttributeGroupLibAm attributeGroupAm, string createdBy = null)
        {
            if (attributeGroupAm == null)
                throw new ArgumentNullException(nameof(attributeGroupAm));

            var validation = attributeGroupAm.ValidateObject();

            if (!validation.IsValid)
                throw new MimirorgBadRequestException("Attribute is not valid.", validation);

            var dm = _mapper.Map<AttributeGroupLibDm>(attributeGroupAm);


            dm.Attributes = new List<AttributeGroupAttributesLibDm>();

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
                        dm.Attributes.Add(new AttributeGroupAttributesLibDm { AttributeGroupId = dm.Id, AttributeId = attribute.Id });
                    }
                }
            }


            dm.CreatedBy = createdBy;

            var createdAttributeGroup = await _attributeGroupRepository.Create(dm, attributeGroupAm.Attributes);
            //_hookService.HookQueue.Enqueue(CacheKey.Attribute);
            _attributeGroupRepository.ClearAllChangeTrackers();
            await _logService.CreateLog(createdAttributeGroup, LogType.Create,createdAttributeGroup?.CreatedBy, createdBy);

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
            //Update name of group and or add attributes

            var validation = attributeAm.ValidateObject();

            if (!validation.IsValid)
                throw new MimirorgBadRequestException("Block is not valid.", validation);

            var attributeGroupToUpdate = _attributeGroupRepository.FindBy(x => x.Id == id, false).Include(x => x.Attributes).AsSplitQuery().FirstOrDefault();

            if (attributeGroupToUpdate == null)
                throw new MimirorgNotFoundException("Block not found. Update is not possible.");

            var attributeGroupToReturn = await Update(attributeGroupToUpdate.Id, attributeAm);

            _attributeGroupRepository.ClearAllChangeTrackers();
            //_hookService.HookQueue.Enqueue(CacheKey.Block);

            return attributeGroupToReturn;
        }

        public Task<ApprovalDataCm> ChangeState(string id, State state, bool sendStateEmail)
        {
            throw new NotImplementedException();
        }

        public void ClearAllChangeTrackers()
        {
            throw new NotImplementedException();
        }
    }
}
