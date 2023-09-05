using AutoMapper;
using Microsoft.AspNetCore.Http;
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
        private readonly IMapper _mapper;
        private readonly IUnitService _unitService;
        private readonly ITimedHookService _hookService;
        private readonly ILogService _logService;


        public AttributeGroupService(IEfAttributeGroupRepository attributeGroupRepository, IMapper mapper, ITimedHookService hookService, ILogService logService, IHttpContextAccessor contextAccessor, IEmailService emailService)
        {
            attributeGroupRepository = _attributeGroupRepository;
            _mapper = mapper;
            _hookService = hookService;
            _logService = logService;
        }

        /// <inheritdoc />
        public async Task<AttributeGroupLibCm> GetSingleAttributeGroup(string id)
        {
            var dm = _attributeGroupRepository.GetSingleAttributeGroup(id);

            if (dm == null)
                throw new MimirorgNotFoundException($"Attribute with id {id} not found.");

            return _mapper.Map<AttributeGroupLibCm>(dm);

        }

        /// <inheritdoc />
        public IEnumerable<AttributeGroupLibCm> GetAttributeGroupList(string searchText = null)
        {
            var dm = _attributeGroupRepository.GetAttributeGroupList(searchText).ToList();
            
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

            if (!string.IsNullOrEmpty(createdBy))
            {
                dm.CreatedBy = createdBy;
                dm.State = State.Approved;
            }
            else
            {
                dm.State = State.Draft;
            }

            var createdAttributeGroup = await _attributeGroupRepository.Create(dm);
            _hookService.HookQueue.Enqueue(CacheKey.Attribute);
            _attributeGroupRepository.ClearAllChangeTrackers();
            await _logService.CreateLog(createdAttributeGroup, LogType.Create, createdAttributeGroup?.State.ToString(), createdAttributeGroup?.CreatedBy);

            return _mapper.Map<AttributeGroupLibCm>(createdAttributeGroup);
        }

        public async Task Delete(string id)
        {
            var dm = _attributeGroupRepository.GetSingleAttributeGroup(id) ?? throw new MimirorgNotFoundException($"Attribute with id {id} not found.");

            if (dm.State == State.Approved)
                throw new MimirorgInvalidOperationException($"Can't delete approved attribute with id {id}.");

            await _attributeGroupRepository.Delete(id);
            await _attributeGroupRepository.SaveAsync();
        }

        Task<IEnumerable<AttributeGroupLibCm>> IAttributeGroupService.GetAttributeGroupList(string searchText)
        {
            throw new NotImplementedException();
        }

   

        public Task<AttributeGroupLibCm> Update(string id, AttributeGroupLibAm attributeAm)
        {
            throw new NotImplementedException();
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
