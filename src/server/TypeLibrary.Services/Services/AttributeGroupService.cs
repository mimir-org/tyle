using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Common.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Contracts;
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
        public async Task<AttributeGroupCm> GetSingleAttributeGroup(string id)
        {
            throw new NotImplementedException();
            
        }

        /// <inheritdoc />
        public Task<IEnumerable<AttributeGroupCm>> GetAttributeGroupList(string searchText = null)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<AttributeGroupCm> Create(AttributeGroupAm attributeAm, string createdBy = null)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }


        /// <inheritdoc />
        public Task<AttributeGroupCm> Update(string id, AttributeGroupAm attributeGroupAm)
        {
            throw new NotImplementedException();
        }
        /// <inheritdoc />
        public Task<ApprovalDataCm> ChangeState(string id, State state, bool sendStateEmail)
        {
            throw new NotImplementedException();
        }
    }
}
